using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class BurialmainTextile
    {
        public long MainBurialmainid { get; set; }
        public Burialmain? Burialmain { get; set; }

        public long MainTextileid { get; set; }
        public Textile? Textile { get; set; }
    }
}
