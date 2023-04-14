using System;
namespace fag_el_gamous.Models
{
	public class AllLinkingTablesViewModel
	{
		public BurialmainTextile? BurialmainSingle { get; set; }
		public List<BurialmainTextile>? BurialmainTextile { get; set; }
		public List<AnalysisTextile>? AnalysisTextile { get; set; }
		public List<ColorTextile>? ColorTextile { get; set; }
		public List<DecorationTextile>? DecorationTextile { get; set; }
		public List<DimensionTextile>? DimensionTextile { get; set; }
		public List<TextilefunctionTextile>? TextilefunctionTextile { get; set; }
		public List<StructureTextile>? StructureTextile { get; set; }
        public List<YarnmanipulationTextile>? YarnmanipulationTextile { get; set; }
        public List<PhotodataTextile>? PhotodataTextile { get; set; }
		public BodyAnalysisChartViewModel? BodyAnalysisViewModel { get; set; }

    }
}

