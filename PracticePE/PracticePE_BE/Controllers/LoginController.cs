using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Repository.Repository;
using Repository.RequestModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PracticePE_BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private IConfiguration _configuration;
		private readonly UnitOfWork _unitOfWork;

		public LoginController(IConfiguration configuration, UnitOfWork unitOfWork)
		{
			_configuration = configuration;
			_unitOfWork = unitOfWork;
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Login([FromBody] UserLogin userLogin)
		{
			var user = Authenticate(userLogin);
			if (user != null)
			{
				var token = GenerateToken(user);
				return Ok(token);
			}
			return NotFound("User not found!");
		}

		private UserAccount Authenticate(UserLogin userLogin)
		{
			var currentUser = _unitOfWork.AccountRepo.Get().FirstOrDefault(u => u.UserEmail == userLogin.UserEmail 
					&& u.UserPassword == userLogin.UserPassword);
			//var currentUser = UserConstants.Users.FirstOrDefault(u => u.UserName.ToLower() == userLogin.UserName.ToLower()
			//		&& u.Password == userLogin.Password);
			if (currentUser != null)
			{
				return currentUser;
			}
			return null;
		}

		private string GenerateToken(UserAccount user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var userRole = user.Role switch
			{
				1 => "Administrator",
				2 => "Staff",
				3 => "Manager",
				4 => "Customer"
			};

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserFullName),
				new Claim(ClaimTypes.Email, user.UserEmail),
				new Claim(ClaimTypes.Role, userRole)
			};

			var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
