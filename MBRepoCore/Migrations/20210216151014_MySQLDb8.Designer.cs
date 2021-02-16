﻿// <auto-generated />
using MBRepoCore.Models_Example;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MBRepoCore.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20210216151014_MySQLDb8")]
    partial class MySQLDb8
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
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Branche");
                });

            modelBuilder.Entity("MBRepoCore.Models_Example.Student", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("BrancheID")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("BrancheID1")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("ID");

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
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

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
