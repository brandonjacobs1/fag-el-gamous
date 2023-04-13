using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class Color
    {
        public long Id { get; set; }
        public string? Value { get; set; }
        public int? Colorid { get; set; }

        public List<ColorTextile> ColorTextiles { get; set; }
    }
}
