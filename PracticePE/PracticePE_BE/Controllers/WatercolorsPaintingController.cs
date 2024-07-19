using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repository;
using Repository.RequestModels;

namespace PracticePE_BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WatercolorsPaintingController : ControllerBase
	{
		private readonly UnitOfWork _unitOfWork;

		public WatercolorsPaintingController(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[Authorize(Roles = "Staff,Manager")]
		[HttpGet]
		public IActionResult GetWatercolorsPaintings([FromQuery] RequestSearchModel requestSearchModel)
		{
			var watercolorsPainting = _unitOfWork.WatercolorsPaintingRepo.Get(includeProperties: "Style");

			if (!string.IsNullOrEmpty(requestSearchModel.PaintingAuthor))
			{
				watercolorsPainting = watercolorsPainting.Where(w => w.PaintingAuthor.ToLower().Contains(requestSearchModel.PaintingAuthor.ToLower()));
			}

			if (requestSearchModel.PublishYear.HasValue)
			{
				watercolorsPainting = watercolorsPainting.Where(w => w.PublishYear == requestSearchModel.PublishYear.Value);
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(watercolorsPainting);
			}

			return Ok(watercolorsPainting);
		}

		[Authorize(Roles = "Manager")]
		[HttpPost]
		public IActionResult PostWatercolorsPaintings(WatercolorsPaintingModel watercolorsPaintingModel)
		{
			var words = watercolorsPaintingModel.PaintingName.Split(' ');
			foreach (var word in words)
			{
				if (!char.IsUpper(word[0]))
				{
					return BadRequest("Each word of the PaintingName must begin with the capital letter");
				}
			}

			var watercolorsPainting = new WatercolorsPainting
			{
				PaintingId = watercolorsPaintingModel.PaintingId,
				PaintingName = watercolorsPaintingModel.PaintingName,
				PaintingDescription = watercolorsPaintingModel.PaintingDescription,
				PaintingAuthor = watercolorsPaintingModel.PaintingAuthor,
				Price = watercolorsPaintingModel.Price,
				PublishYear = watercolorsPaintingModel.PublishYear,
				CreatedDate = DateTime.Now,
				StyleId = watercolorsPaintingModel.StyleId
			};
			_unitOfWork.WatercolorsPaintingRepo.Insert(watercolorsPainting);
			_unitOfWork.Save();
			return Ok(watercolorsPainting);
		}

		[Authorize(Roles = "Manager")]
		[HttpPut("{id}")]
		public IActionResult PutWatercolorsPaintings(string id, WatercolorsPaintingModel watercolorsPaintingModel)
		{
			if (id != watercolorsPaintingModel.PaintingId)
			{
				return BadRequest();
			}

			if (!_unitOfWork.WatercolorsPaintingRepo.Get().Any(w => w.PaintingId == id))
			{
				return NotFound();
			}
			var existing = _unitOfWork.WatercolorsPaintingRepo.Get().FirstOrDefault(e => e.PaintingId == id);

			var words = watercolorsPaintingModel.PaintingName.Split(' ');
			foreach (var word in words)
			{
				if (!char.IsUpper(word[0]))
				{
					return BadRequest("Each word of the PaintingName must begin with the capital letter");
				}
			}

			if (existing != null)
			{
				existing.PaintingName = watercolorsPaintingModel.PaintingName;
				existing.PaintingDescription = watercolorsPaintingModel.PaintingDescription;
				existing.PaintingAuthor = watercolorsPaintingModel.PaintingAuthor;
				existing.Price = watercolorsPaintingModel.Price;
				existing.PublishYear = watercolorsPaintingModel.PublishYear;
				existing.CreatedDate = DateTime.Now;
				existing.StyleId = watercolorsPaintingModel.StyleId;
			};
			_unitOfWork.WatercolorsPaintingRepo.Update(existing);
			_unitOfWork.Save();
			return Ok(existing);
		}

		[Authorize(Roles = "Manager")]
		[HttpDelete("{id}")]
		public IActionResult DeleteWatercolorsPainting(string id)
		{
			var watercolorsPainting = _unitOfWork.WatercolorsPaintingRepo.Get().FirstOrDefault(w => w.PaintingId == id);
			if (watercolorsPainting == null)
			{
				return NotFound();
			}
			_unitOfWork.WatercolorsPaintingRepo.Delete(watercolorsPainting);
			_unitOfWork.Save();
			return Ok(watercolorsPainting);
		}
	}
}
