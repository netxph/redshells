using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RedShells.Data
{

    public class DataContext : DbContext
    {
        public DbSet<Workspace> Workspaces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./redshells.db");
        }
    }
}
