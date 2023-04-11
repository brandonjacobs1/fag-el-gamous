using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fag_el_gamous.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    //public DbSet<IdentityRole> Roles { get; set; } = null!;

    //protected override void OnModelCreating(ModelBuilder mb)
    //{
    //    mb.Entity<IdentityRole>().HasData
    //        (
    //            new IdentityRole
    //            {
    //                Id = "1",
    //                ConcurrencyStamp = "4/11/2023",
    //                Name = "Admin",
    //                NormalizedName = "Admin"
    //            },
    //            new IdentityRole
    //            {
    //                Id = "2",
    //                ConcurrencyStamp = "4/11/2023",
    //                Name = "Researcher",
    //                NormalizedName = "Researcher"
    //            }
    //        );
    //}
}

