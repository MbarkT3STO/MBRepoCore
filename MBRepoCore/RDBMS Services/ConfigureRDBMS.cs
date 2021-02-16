using System;
using System.Collections.Generic;
using System.Text;
using MBRepoCore.Factories;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.RDBMS_Services
{
    public static class ConfigureRdbms
    {
        /// <summary>
        ///  Configure a <b><see cref="IDbContextInstanceOptions"/> <b><see cref="DbContextOptionsBuilder{TContext}"/></b> to use MySQL</b>
        /// </summary>
        ///  <param name="instanceOptions">A <b><see cref="IDbContextInstanceOptions"/></b> instance as reference</param>
        public static void ConfigureSqlServer(ref IDbContextInstanceOptions instanceOptions)
        {
            instanceOptions.OptionsBuilder.UseSqlServer(instanceOptions.connectionString);
        }


        ///  <summary>
        ///  Configure a <b><see cref="IDbContextInstanceOptions"/> <b><see cref="DbContextOptionsBuilder{TContext}"/></b> to use MySQL</b>
        ///  </summary>
        ///  <param name="instanceOptions">A <b><see cref="IDbContextInstanceOptions"/></b> instance as reference</param>
        public static void ConfigureMySQL(ref IDbContextInstanceOptions instanceOptions)
        {
            instanceOptions.OptionsBuilder.UseMySql(instanceOptions.connectionString, ServerVersion.AutoDetect(instanceOptions.connectionString));
        }


        ///  <summary>
        ///  Configure a <b><see cref="IDbContextInstanceOptions"/> <b><see cref="DbContextOptionsBuilder{TContext}"/></b> to use Oracle</b>
        ///  </summary>
        ///  <param name="instanceOptions">A <b><see cref="IDbContextInstanceOptions"/></b> instance as reference</param>
        public static void ConfigureOracle(ref IDbContextInstanceOptions instanceOptions)
        {
            instanceOptions.OptionsBuilder.UseOracle(instanceOptions.connectionString);
        }
    }
}
