﻿using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class AnalysisTextile
    {
        public long MainAnalysisid { get; set; }
        public Analysis? Analysis { get; set; }

        public long MainTextileid { get; set; }
        public Textile? Textile { get; set; }

    }
}
