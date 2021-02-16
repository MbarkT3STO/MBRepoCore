using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql;

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

            // To use SQL Server
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MBARKServer"));

            // To use MySQL
            //optionsBuilder.UseMySql(configuration.GetConnectionString("MySQL"), ServerVersion.AutoDetect(configuration.GetConnectionString("MySQL")));
        }

        public DbSet<Branche> Branches { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
