using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class YarnmanipulationTextile
    {
        public long MainYarnmanipulationid { get; set; }
        public Yarnmanipulation? Yarnmanipulation { get; set; }
        public long MainTextileid { get; set; }
        public Textile? Textile { get; set; }
    }
}
