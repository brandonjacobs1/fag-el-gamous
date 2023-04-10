using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class Bodyanalysischart
    {
        public int Id { get; set; }
        public DateOnly? Dateofexamination { get; set; }
        public int? Preservationindex { get; set; }
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
        public string? Preauricularsulcus { get; set; }
        public string? MedialIpRamus { get; set; }
        public string? Dorsalpitting { get; set; }
        public string? Femur { get; set; }
        public string? Humerus { get; set; }
        public int? Femurheaddiameter { get; set; }
        public int? Humerusheaddiameter { get; set; }
        public int? Femurlength { get; set; }
        public int? Humeruslength { get; set; }
        public int? Estimatestature { get; set; }
        public string? Osteophytosis { get; set; }
        public string? CariesPeriodontalDisease { get; set; }
        public string? Notes { get; set; }
    }
}
