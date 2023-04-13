using System;
using Microsoft.AspNetCore.Identity;

namespace fag_el_gamous.Models
{
	public class UserRolesView
	{
        public IdentityUser User { get; set; }
        public IdentityRole SingleRole { get; set; }
        public IList<string> Roles { get; set; }
        public List<IdentityRole> ListRoles { get; set; } = null!;
        public List<IdentityUser> ListUsers { get; set; } = null!;
        public string NewRole { get; set; } = null!;
    }
}

