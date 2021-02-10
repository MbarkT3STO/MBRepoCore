using MBRepoCore.Models_Example;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MBRepoCore
{
    class SchoolContext2 : DbContext, IDbContextFactory<SchoolContext2>
    {
        public SchoolContext2()
        {

        }
        public SchoolContext2(DbContextOptions<SchoolContext2> dbContextOptions) : base(dbContextOptions)
        {

        }

        public SchoolContext2 GetInstance(IConfiguration configuration, string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext2>();
            optionsBuilder.UseSqlServer(connectionString);
            return new SchoolContext2(optionsBuilder.Options);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Relace [MBARK\\MBARK_SERVER] by your server name
            //Relace [MySchoolDB] by your own/Suggested database name
            optionsBuilder.UseSqlServer("Server=MBARK\\MBARK_SERVER;Database=MySchoolDB;Trusted_Connection=True;");
        }

        public DbSet<Branche> Branches { get; set; }

    }
}