using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class StructureTextile
    {
        public long MainStructureid { get; set; }
        public Structure? Structure { get; set; }
        public long MainTextileid { get; set; }
        public Textile? Textile { get; set; }
    }

}
