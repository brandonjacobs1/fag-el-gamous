using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class DecorationTextile
    {
        public long MainDecorationid { get; set; }
        public Decoration? Decoration { get; set; }
        public long MainTextileid { get; set; }
        public Textile? Textile { get; set; }
    }
}
