using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class Dimension
    {
        public long Id { get; set; }
        public string? Dimensiontype { get; set; }
        public string? Value { get; set; }
        public int? Dimensionid { get; set; }
    }
}
