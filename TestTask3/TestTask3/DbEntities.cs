using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask3
{
	public class DbEntities : DbContext
	{
		public static readonly string connectionstring = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog= IncidentsDb;";

       public DbSet<Incident> Incidents { get; set; }
       public DbSet<Account> Accounts { get; set; }
       public DbSet<Contact> Contacts { get; set; }
       public DbEntities() 
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbContextOptions<DbEntities> _options;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionstring);
        }

    }
}
