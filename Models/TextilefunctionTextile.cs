using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class TextilefunctionTextile
    {
        public long MainTextilefunctionid { get; set; }
        public Textilefunction? Textilefunction { get; set; }
        public long MainTextileid { get; set; }
        public Textile? Textile { get; set; }
    }
}
