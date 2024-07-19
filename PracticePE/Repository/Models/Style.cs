using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Style
    {
        public Style()
        {
            WatercolorsPaintings = new HashSet<WatercolorsPainting>();
        }

        public string StyleId { get; set; }
        public string StyleName { get; set; }
        public string StyleDescription { get; set; }
        public string OriginalCountry { get; set; }

        public virtual ICollection<WatercolorsPainting> WatercolorsPaintings { get; set; }
    }
}
