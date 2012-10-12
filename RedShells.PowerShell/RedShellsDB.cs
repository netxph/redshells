using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace RedShells.PowerShell
{
    public class RedShellsDB : DbContext
    {

        public RedShellsDB()
        {

        }

        static RedShellsDB()
        {
            Database.DefaultConnectionFactory = new LocalDbConnectionFactory("v11.0");
        }

        public DbSet<Workspace> Workspaces { get; set; }

    }
}
