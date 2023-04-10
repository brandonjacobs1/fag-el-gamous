using System;
using Microsoft.EntityFrameworkCore;
using fag_el_gamous.Models;

namespace fag_el_gamous.Data
{
	public class MummyContext : DbContext
	{
		public MummyContext(DbContextOptions<MummyContext> options)
        : base(options)
		{
		}

        public DbSet<Mummy>? Mummies { get; set; }
    }
	
}

