﻿using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class ColorTextile
    {
        public long MainColorid { get; set; }
        public Color? Color { get; set; }
        public long MainTextileid { get; set; }
        public Textile? Textile { get; set; }
    }
}
