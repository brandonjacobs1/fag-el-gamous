﻿using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class DimensionTextile
    {
        public long MainDimensionid { get; set; }
        public Dimension? Dimension { get; set; }
        public long MainTextileid { get; set; }
        public Textile? Textile { get; set; }
    }
}
