﻿// <auto-generated />
using MBRepoCore.Models_Example;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MBRepoCore.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20210216141129_newMySQL")]
    partial class newMySQL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("MBRepoCore.Models_Example.Branche", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID")
                        .HasName("PK_Branche");

                    b.ToTable("Branche");
                });

            modelBuilder.Entity("MBRepoCore.Models_Example.Student", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("BrancheID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("BrancheID1")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID")
                        .HasName("PK_Student");

                    b.HasIndex("BrancheID");

                    b.HasIndex("BrancheID1");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("MBRepoCore.Models_Example.Student", b =>
                {
                    b.HasOne("MBRepoCore.Models_Example.Branche", null)
                        .WithMany()
                        .HasForeignKey("BrancheID")
                        .HasConstraintName("FK_Student_Branche")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MBRepoCore.Models_Example.Branche", "Branche")
                        .WithMany("Students")
                        .HasForeignKey("BrancheID1");

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
