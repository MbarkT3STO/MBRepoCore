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
        /// Get a new nstance from <b><typeparamref name="TContext"/></b>
        /// <para>The <b><typeparamref name="TContext"/></b> should implement this Interface first</para>
        /// </summary>
        /// <param name="configuration">An <b><see cref="IConfiguration"/></b> object</param>
        /// <param name="connectionString">
        /// <para>The connection string for your SQL Server database</para>
        ///<example>
        /// <para><b>Example</b></para>
        ///
        /// <para>The connection string can be :</para>
        ///<code>Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;</code>
        /// 
        /// <para>Or can be :</para>
        /// <code>configuration.GetConnectionString("DB connection part in connectionStrings inside appSettings.json")</code>
        /// </example>
        /// </param>
        /// <returns></returns>
        TContext GetInstance(IConfiguration configuration, string connectionString);

    }
}
