using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MBRepoCore.Models_Example
{
    class SchoolContext:DbContext
    {
        public SchoolContext()
        {
            
        } 
        public SchoolContext(DbContextOptions<SchoolContext> dbContextOptions):base(dbContextOptions)
        {
            
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            IConfiguration configuration = new ConfigurationBuilder()
                                          .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                                          .AddJsonFile("appsettings.json", false).Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MBARKServer"));
        }

        public DbSet<Branche> Branches { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
