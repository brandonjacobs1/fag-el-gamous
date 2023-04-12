using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace fag_el_gamous.Models
{
	public class Application
	{
        public List<IdentityRole> Roles { get; set; } = null!;
        public List<IdentityUser> Users { get; set; } = null!;
        public string NewRole { get; set; } = null!;
    }
}

