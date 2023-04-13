﻿using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class Bodyanalysischart
    {
        public int Key { get; set; }
        public int Squarenorthsouth { get; set; }
        public string Northsouth { get; set; } = null!;
        public int Squareeastwest { get; set; }
        public string Eastwest { get; set; } = null!;
        public string? Area { get; set; }
        public int Burialnumber { get; set; }
        public string? Dateofexamination { get; set; }
        public decimal? Preservationindex { get; set; }
        public string? Haircolor { get; set; }
        public string? Observations { get; set; }
        public string? Robust { get; set; }
        public string? Supraorbitalridges { get; set; }
        public string? Orbitedge { get; set; }
        public string? Parietalbossing { get; set; }
        public string? Gonion { get; set; }
        public string? Nuchalcrest { get; set; }
        public string? Zygomaticcrest { get; set; }
        public string? Sphenooccipitalsynchrondrosis { get; set; }
        public string? Lamboidsuture { get; set; }
        public string? Squamossuture { get; set; }
        public string? Toothattrition { get; set; }
        public string? Tootheruption { get; set; }
        public string? Tootheruptionageestimate { get; set; }
        public string? Ventralarc { get; set; }
        public string? Subpubicangle { get; set; }
        public string? Sciaticnotch { get; set; }
        public string? Pubicbone { get; set; }
        public string? PreauricularsulcusBoolean { get; set; }
        public string? MedialIpRamus { get; set; }
        public string? DorsalpittingBoolean { get; set; }
        public string? Femur { get; set; }
        public string? Humerus { get; set; }
        public string? Femurheaddiameter { get; set; }
        public string? Humerusheaddiameter { get; set; }
        public decimal? Femurlength { get; set; }
        public decimal? Humeruslength { get; set; }
        public string? Estimatestature { get; set; }
        public string? Osteophytosis { get; set; }
        public string? CariesPeriodontalDisease { get; set; }
        public string? Notes { get; set; }
    }
}
