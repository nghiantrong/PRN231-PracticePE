using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class WatercolorsPainting
    {
        public string PaintingId { get; set; }
        public string PaintingName { get; set; }
        public string PaintingDescription { get; set; }
        public string PaintingAuthor { get; set; }
        public decimal? Price { get; set; }
        public int? PublishYear { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string StyleId { get; set; }

        public virtual Style Style { get; set; }
    }
}
