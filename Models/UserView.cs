using System;
using System.ComponentModel.DataAnnotations;

namespace fag_el_gamous.Models
{
	public class UserView
	{
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}

