using System;
using System.ComponentModel.DataAnnotations;

namespace fag_el_gamous.Models
{
    public class filterViewModel
    {
        
        public PaginatedList<idknameyet>? displayBurial { get; set; }
        
        public userSearch? Search { get; set; }
        

    }
}

