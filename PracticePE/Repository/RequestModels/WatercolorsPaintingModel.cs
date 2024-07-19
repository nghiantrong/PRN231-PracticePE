using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RequestModels
{
	public class WatercolorsPaintingModel
	{
		[Required]
		public string PaintingId { get; set; }
		[Required]
		public string PaintingName { get; set; }
		[Required]
		public string PaintingDescription { get; set; }
		[Required]
		public string PaintingAuthor { get; set; }
		[Required]
		[Range(0, double.MaxValue, ErrorMessage = "Price must be >= 0")]
		public decimal Price { get; set; }
		[Required]
		[Range(1000, int.MaxValue, ErrorMessage = "PublishYear must be >= 1000")]
		public int PublishYear { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }
		[Required]
		public string StyleId { get; set; }
	}
}
