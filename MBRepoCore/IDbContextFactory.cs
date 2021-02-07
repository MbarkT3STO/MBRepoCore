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
        /// <param name="configuration"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        TContext GetInstance(IConfiguration configuration, string connectionString);

    }
}
