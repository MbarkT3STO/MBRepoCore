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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Map entities to tables
            //modelBuilder.Entity<Branche>().ToTable("Branche");
            //modelBuilder.Entity<Student>().ToTable("Student");

            //// Configure Primary Keys  
            //modelBuilder.Entity<Branche>().HasKey(b => b.ID).HasName("PK_Branche");
            //modelBuilder.Entity<Student>().HasKey(s => s.ID).HasName("PK_Student");

            // Configure columns

            /*For Branche*/
            modelBuilder.Entity<Branche>().Property(b => b.ID).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Branche>().Property(b => b.Title).HasColumnType("nvarchar(50)").IsRequired();

            /*For Student*/
            modelBuilder.Entity<Student>().Property(s => s.ID).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.Name).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.BrancheID).HasColumnType("nvarchar(50)").IsRequired();


            ////Configure Relations
            //modelBuilder.Entity<Student>().HasOne<Branche>().WithMany().HasPrincipalKey(b => b.ID)
            //            .HasForeignKey(s => s.BrancheID).OnDelete(DeleteBehavior.NoAction)
            //            .HasConstraintName("FK_Student_Branche");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            IConfiguration configuration = new ConfigurationBuilder()
                                          .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                                          .AddJsonFile("appsettings.json", false).Build();

            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("MBARKServer"));
            optionsBuilder.UseMySql(configuration.GetConnectionString("MySQL"), ServerVersion.AutoDetect(configuration.GetConnectionString("MySQL")));
        }

        public DbSet<Branche> Branches { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
