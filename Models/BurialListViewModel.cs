using System;
using X.PagedList;
namespace fag_el_gamous.Models
{
	public class BurialListViewModel
	{
		public PaginatedList<displayBurial>? displayBurial { get; set; }
		public userSearch? userSearch { get; set;}
        //public PaginatedList<displayBurial>? PageInfo { get; set; }
    }
}

