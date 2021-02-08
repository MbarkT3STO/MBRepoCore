using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MBRepoCore
{
    public interface IDbContextFactory<out TContext>
    {
        /// <summary>
        /// Get a new nstance from <typeparamref name="TContext"/>
        /// </summary>
        /// <param name="configuration">The <typeparamref name="IConfiguration"/> object </param>
        /// <param name="connectionString">Connection string as Section from settings file or as string expression
        /// </param>
        /// <returns></returns>
        TContext GetInstance(IConfiguration configuration, string connectionString);

    }
}
