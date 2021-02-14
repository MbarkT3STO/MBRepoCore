using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Factories
{
    /// <summary>
    /// A factory that responsible about creating new instances from <b><see cref="DbContext"/></b> contexts
    /// </summary>
    /// <typeparam name="TContext">The dbcontext as type</typeparam>
    public static class RepoDBContextFactory<TContext> where TContext:DbContext

    {

        /// <summary>
        /// Create a new instance from <b><see cref="TContext"/></b> where it hasn't any parameter
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
        /// Create a new instance from <b><see cref="TContext"/></b> where it has a <b><see cref="DbContextOptions"/></b> parameter
        /// </summary>
        /// <typeparam name="TContext">The dbcontext as type</typeparam>
        /// <param name="options">The dbcontext options</param>
        /// <returns></returns>
        public static TContext GetInstance(DbContextOptions options)
        {
            lock ("")
            {
                TContext context = (TContext) Activator.CreateInstance(typeof(TContext), new object[] {options});
 
                return context;
            }
        }
        

        /// <summary>
        /// Create a new instance from <b><see cref="TContext"/></b> from a connection string
        /// </summary>
        /// <typeparam name="TContext">The dbcontext as type</typeparam>
        /// <param name="connectionString">The sql server database connection string</param>
        /// <returns></returns>
        public static TContext GetInstance(string connectionString)
        {
            lock ("")
            {
                var optionsBuilder = new DbContextOptionsBuilder<TContext>();
                optionsBuilder.UseSqlServer(connectionString);
                TContext context = (TContext) Activator.CreateInstance(typeof(TContext), optionsBuilder.Options);
 
                return context;
            }
        }

    }
}
