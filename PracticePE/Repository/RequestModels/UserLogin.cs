using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RequestModels
{
	public class UserLogin
	{
		public string UserEmail { get; set; }
		public string UserPassword { get; set; }
	}
}
