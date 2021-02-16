﻿// <auto-generated />
using MBRepoCore.Models_Example;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MBRepoCore.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20210216135022_SecondMig")]
    partial class SecondMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MBRepoCore.Models_Example.Branche", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("MBRepoCore.Models_Example.Student", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BrancheID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BrancheID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("MBRepoCore.Models_Example.Student", b =>
                {
                    b.HasOne("MBRepoCore.Models_Example.Branche", "Branche")
                        .WithMany("Students")
                        .HasForeignKey("BrancheID");

                    b.Navigation("Branche");
                });

            modelBuilder.Entity("MBRepoCore.Models_Example.Branche", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
