using System;
namespace fag_el_gamous.Models
{
	public class AdvancedFilterViewModel
	{
		public PaginatedList<displayBurial>? burialmains {get; set;}
        public PaginatedList<filterLinkingTables>? filterLinkingTables { get; set; }
        public userSearch? Search { get; set; }

    }
}

