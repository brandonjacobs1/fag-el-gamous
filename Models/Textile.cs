using System;
using System.Collections.Generic;

namespace fag_el_gamous.Models
{
    public partial class Textile
    {
        public long Id { get; set; }
        public string? Locale { get; set; }
        public int? Textileid { get; set; }
        public string? Description { get; set; }
        public string? Burialnumber { get; set; }
        public string? Estimatedperiod { get; set; }
        public DateTime? Sampledate { get; set; }
        public DateTime? Photographeddate { get; set; }
        public string? Direction { get; set; }

        public List<BurialmainTextile>? BurialmainTextiles { get; set; }
        public List<AnalysisTextile>? AnalysisTextiles { get; set; }
        public List<ColorTextile>? ColorTextiles { get; set; }
        public List<DecorationTextile>? DecorationTextiles { get; set; }
        public List<DimensionTextile>? DimensionTextiles { get; set; }
        public List<TextilefunctionTextile>? TextilefunctionsTextiles { get; set; }
        public List<StructureTextile>? StructureTextiles { get; set; }
        public List<YarnmanipulationTextile>? YarnmanipulationTextiles { get; set; }
        public List<PhotodataTextile>? photodataTextiles { get; set; }
        
    }
}
