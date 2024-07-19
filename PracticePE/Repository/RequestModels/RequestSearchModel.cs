using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RequestModels
{
	public class RequestSearchModel
	{
		public string? PaintingAuthor { get; set; }
		public int? PublishYear { get; set; }
	}
}
