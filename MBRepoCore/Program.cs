using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MBRepoCore.Exceptions;
using MBRepoCore.Models_Example;
using MBRepoCore.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MBRepoCore
{
    class Program
    {
        private static IConfiguration _configuration;

        private static void ConfigureServices()
        {
            _configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                            .AddJsonFile("appsettings.json", false).Build();
        }

        static async Task Main(string[] args)
        {
            //Configure the DI
            ConfigureServices();


            //--------------------------------
            //Create repo objets
            //--------------------------------
            /*--------*/
            /*Method 1*/
            /*--------*/
            //var repo = new Repo<SchoolContext>(false);

            /*--------*/
            /*Method 2*/
            /*--------*/
            var repo = new Repo<SchoolContext>(_configuration.GetConnectionString("MBARKLap"), false);


            //----------------
            //Get Students
            //----------------
            var students = repo.GetAll<Student>();
            foreach (var s in students)
            {
                Console.WriteLine($"ID : {s.ID}, Name : {s.Name}, Branche : {s.BrancheID}");
            }




            //----------------
            //GeT ONE Student
            //----------------
            //var s = repo.GetOne<Student>("S-3");
            //Console.WriteLine($"{s.ID} {s.Name} {s.BrancheID}");




            //-------------------------------------------
            //Insert one Student record into a student table
            //-------------------------------------------
            //var s = new Student()
            //        {
            //            ID        = "S-XX",
            //            Name      = "Mohammad",
            //            BrancheID = "IT",
            //        };

            ////Can use
            ////repo.Insert<Student>(s);

            //Or use
            //repo.Insert(s);

            //repo.Save();
            //Console.WriteLine("Done !");


            //--------------------------------------------------
            //Insert a range of Branches into branche table
            //--------------------------------------------------

            //var branches = new List<Branche>()
            //               {
            //                   new Branche() {ID = "IT", Title    = "Information Technology"},
            //                   new Branche() {ID = "MATH", Title  = "Maths"},
            //                   new Branche() {ID = "HS", Title    = "History"},
            //                   new Branche() {ID = "PHILO", Title = "Philosophy"},
            //               };

            //await repo.InsertRangeAsync(branches);
            //repo.Save();
            //Console.WriteLine("Done !");




            //--------------------------------------------------
            //Insert a range of Students into student table
            //--------------------------------------------------
            //var students = new List<Student>()
            //               {
            //                   new Student()
            //                   {
            //                       ID        = "S-1",
            //                       Name       = "Mohammad",
            //                       BrancheID = "IT",
            //                   },
            //                   new Student()
            //                   {
            //                       ID        = "S-2",
            //                       Name       = "Ahmed",
            //                       BrancheID = "HS",
            //                   },
            //                   new Student()
            //                   {
            //                       ID        = "S-3",
            //                       Name       = "Ibtissam",
            //                       BrancheID = "IT",
            //                   },
            //                   new Student()
            //                   {
            //                       ID        = "S-4",
            //                       Name       = "kARIM",
            //                       BrancheID = "MATH",
            //                   },

            //               };
            //await repo.InsertRangeAsync(students);
            //repo.Save();
            //Console.WriteLine("Done !");



            //--------------------------------------------------
            //Get many | search by a custom property
            //--------------------------------------------------
            //var R = repo.GetMany<Student>(typeof(Student).GetProperty("BrancheID").Name, "IT");
            //foreach (var s in R)
            //{
            //    Console.WriteLine($"{s.ID} {s.Name} {s.BrancheID}");
            //}





            Console.ReadKey();
        }

    }
}
