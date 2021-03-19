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
        ///  <param name="dbContextInstanceOptions">A <b><see cref="IDbContextInstanceOptions"/></b> instance as reference</param>
        public static void ConfigureSqlServer(ref IDbContextInstanceOptions dbContextInstanceOptions)
        {
            dbContextInstanceOptions.OptionsBuilder.UseSqlServer(dbContextInstanceOptions.ConnectionString);
        }


        ///  <summary>
        ///  Configure a <b><see cref="IDbContextInstanceOptions"/> <b><see cref="DbContextOptionsBuilder{TContext}"/></b> to use MySQL</b>
        ///  </summary>
        ///  <param name="dbContextInstanceOptions">A <b><see cref="IDbContextInstanceOptions"/></b> instance as reference</param>
        public static void ConfigureMySql(ref IDbContextInstanceOptions dbContextInstanceOptions)
        {
            dbContextInstanceOptions.OptionsBuilder.UseMySql(dbContextInstanceOptions.ConnectionString, ServerVersion.AutoDetect(dbContextInstanceOptions.ConnectionString));
        }


        ///  <summary>
        ///  Configure a <b><see cref="IDbContextInstanceOptions"/> <b><see cref="DbContextOptionsBuilder{TContext}"/></b> to use Oracle</b>
        ///  </summary>
        ///  <param name="dbContextInstanceOptions">A <b><see cref="IDbContextInstanceOptions"/></b> instance as reference</param>
        public static void ConfigureOracle(ref IDbContextInstanceOptions dbContextInstanceOptions)
        {
            dbContextInstanceOptions.OptionsBuilder.UseOracle(dbContextInstanceOptions.ConnectionString);
        }
    }
}
