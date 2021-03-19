using System;
using System.Collections.Generic;
using System.Text;
using MBRepoCore.Enums;
using MBRepoCore.RDBMS_Services;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Factories
{
    /// <summary>
    /// A factory that responsible about creating new instances from <b><see cref="DbContext"/></b> contexts
    /// </summary>
    /// <typeparam name="TContext">The dbcontext as type</typeparam>
    public static class RepoDbContextFactory<TContext> where TContext:DbContext

    {

        /// <summary>
        /// Create a new instance from <b><see cref="TContext"/></b> where it hasn't any constructor parameter
        /// <para>This constructor can be used with a <b>DB First</b> context</para>
        /// </summary>
        /// <typeparam name="TContext">The dbcontext as type</typeparam>
        /// <returns></returns>
        public static TContext GetInstance()
        {
            lock ("")
            {
                TContext context = (TContext)Activator.CreateInstance(typeof(TContext));

                return context;
            }
        }


        /// <summary>
        /// Create a new instance from <b><see cref="TContext"/></b> where it has a <b><see cref="DbContextOptions"/></b> constructor parameter
        /// </summary>
        /// <typeparam name="TContext">The dbcontext as type</typeparam>
        /// <param name="dbContextInstanceOptions">A <see cref="IDbContextInstanceOptions"/> object that contains a <see cref="TContext"/> configurations</param>
        /// <returns></returns>
        public static TContext GetInstance(IDbContextInstanceOptions dbContextInstanceOptions)
        {
            //Configure the DbContext RDBMS provider
            ConfigureRdbmsProvider(ref dbContextInstanceOptions);

            lock ("")
            {
                TContext context = (TContext) Activator.CreateInstance(typeof(TContext), new object[] {dbContextInstanceOptions.OptionsBuilder.Options});
 
                return context;
            }
        }



        #region Private methods

        


        /// <summary>
        /// An intermediate that configure <see cref="IDbContextInstanceOptions"/> <see cref="DbContextOptionsBuilder"/> with a specific RDBMS/<b><see cref="RdbmsProvider"/></b>
        /// </summary>
        /// <param name="dbContextInstanceOptions">a <b><see cref="IDbContextInstanceOptions"/></b> object as reference</param>
        private static void ConfigureRdbmsProvider(ref IDbContextInstanceOptions dbContextInstanceOptions)
        {

            switch (dbContextInstanceOptions.RdbmsProvider)
            {
                case RdbmsProvider.SqlServer: ConfigureRdbms.ConfigureSqlServer(ref dbContextInstanceOptions);
                    break;
                case RdbmsProvider.MySql: ConfigureRdbms.ConfigureMySql(ref dbContextInstanceOptions);
                    break;
                case RdbmsProvider.Oracle: ConfigureRdbms.ConfigureOracle(ref dbContextInstanceOptions);
                    break;

            }

        }





        #endregion


    }
}
