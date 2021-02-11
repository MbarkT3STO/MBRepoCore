using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBRepoCore.Factories;
using MBRepoCore.Models_Example;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

namespace MBRepoCore.Repo
{

    /// <summary>
    ///Full generic repository
    /// </summary>
    /// <typeparam name="TContext">The <b><see cref="DbContext"/></b> type</typeparam>
    public sealed class Repo<TContext> : IRepo<TContext>, IDisposable where TContext : DbContext, new()
    {




        #region properties

        /// <summary>
        /// A <b><see cref="TContext"/></b> object as property
        /// </summary>
        public DbContext Context { get; } = null;


        /// <summary>
        /// Determine if Lazy Loading either activate or not
        /// </summary>
        private bool _LazyLoaded { get; set; }

        #endregion




        #region Construcors

        /// <summary>
        /// Use this constructor in Winforms or console application
        /// </summary>
        /// <param name="LazyLoaded">Determine if lazy loading whether active or not</param>
        public Repo(bool LazyLoaded)
        {
            Context                                  = new TContext();
            _LazyLoaded                               = LazyLoaded;
            Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
        }

        /// <summary>
        /// Use this constructor in both Winform or console or ASP.net Core application
        /// </summary>
        /// <param name="context">Object from <b><see cref="TContext"/></b></param>
        /// <param name="LazyLoaded">Determine if lazy loading whether active or not</param>
        public Repo(DbContext context,bool LazyLoaded)
        {
            Context                                  = context;
            _LazyLoaded                               = LazyLoaded;
            Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
        }

        /// <summary>
        /// Use this constructor in ASP.net Core application
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
        /// <param name="LazyLoaded">Determine if lazy loading whether active or not</param>
        public Repo(IConfiguration configuration, string connectionString,bool LazyLoaded)
        {
            Context                                  = RepoDBContextFactory.GetInstance<SchoolContext>(connectionString);
            _LazyLoaded                              = LazyLoaded;
            Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
        }


        public Repo(string connectionString, bool LazyLoaded)
        {
            Context                                  = RepoDBContextFactory.GetInstance<SchoolContext>(connectionString);
            _LazyLoaded                              = LazyLoaded;
            Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
        }

        #endregion




        #region Routines




        #region Select

        /// <summary>
        /// Get All records from a table
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
            {
                return Context.Set<TEntity>().ToList();
            }



            /// <summary>
            /// Asynchronously, Get All records from a table
            /// </summary>
            /// <typeparam name="TEntity">The entity to select from</typeparam>
            /// <returns></returns>
            public Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class
            {
                return Task.Factory.StartNew(() => GetAll<TEntity>());
            }




            /// <summary>
            /// Get One record from a table, based on the primary key value
            /// </summary>
            /// <typeparam name="TEntity">The entity to select from</typeparam>
            /// <param name="pkValue">The primary key value</param>
            /// <returns></returns>
            public TEntity GetOne<TEntity>(object pkValue) where TEntity : class
            {
                return Context.Set<TEntity>().Find(pkValue);
            }



            /// <summary>
            /// Asynchronously, Get One record from a table, based on the primary key value
            /// </summary>
            /// <typeparam name="TEntity">The entity to select from</typeparam>
            /// <param name="pkValue">The primary key value</param>
            /// <returns></returns>
            public Task<TEntity> GetOneAsync<TEntity>(object pkValue) where TEntity : class
            {
                return Task.Factory.StartNew(() => GetOne<TEntity>(pkValue));
            }






            #region Preview feature


             /// <summary>
            /// Get Many records from a table based on a property value
            /// </summary>
            /// <typeparam name="TEntity">The entity to select from</typeparam>
            /// <param name="prop">The property to be used in the condition</param>
            /// <param name="val">The value to be used in the search</param>
            /// <returns></returns>
            public IEnumerable<TEntity> GetMany<TEntity>(string prop, object val) where TEntity : class
            {
                return Context.Set<TEntity>().AsEnumerable()
                               .Where(x => typeof(TEntity).GetProperty(prop).GetValue(x, null).ToString()
                                                          .Contains(val.ToString())).ToList();
            }

        #endregion




        #endregion



        #region Contains

        /// <summary>
        /// Check if a entity contains an object
        /// </summary>
        /// <typeparam name="TEntity">Entity to be look in</typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns></returns>
        public bool Contains<TEntity>(TEntity obj) where TEntity : class
        {
            return Context.Set<TEntity>().AsEnumerable().Contains(obj);
        }


        /// <summary>
        /// Asynchronously Check if a entity contains an object
        /// </summary>
        /// <typeparam name="TEntity">Entity to be look in</typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns></returns>
        public Task<bool> ContainsAsync<TEntity>(TEntity obj) where TEntity : class
        {
            return Task.Factory.StartNew(() => Contains<TEntity>(obj));
        }




        /// <summary>
        ///  Check if a entity contains an object based on a custom EQUALITY Comparer
        /// </summary>
        /// <typeparam name="TEntity">Entity to be look in</typeparam>
        /// <typeparam name="TEntityComparer">The custom TEntity EQUALITY Comparer</typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns></returns>
        public bool Contains<TEntity, TEntityComparer>(TEntity obj)
            where TEntity : class
            where TEntityComparer : IEqualityComparer<TEntity>, new()
        {
            return Context.Set<TEntity>().AsEnumerable().Contains(obj,new TEntityComparer() as IEqualityComparer<TEntity>);
        }



        /// <summary>
        ///  Asynchronously Check if a entity contains an object based on a custom EQUALITY Comparer
        /// </summary>
        /// <typeparam name="TEntity">Entity to be look in</typeparam>
        /// <typeparam name="TEntityComparer">The custom TEntity EQUALITY Comparer</typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns></returns>
        public Task<bool> ContainsAsync<TEntity, TEntityComparer>(TEntity obj)
            where TEntity : class
            where TEntityComparer : IEqualityComparer<TEntity>, new()
        {
            return Task.Factory.StartNew(() => Contains<TEntity, TEntityComparer>(obj));
        }

        #endregion



        #region Insert



        /// <summary>
        /// Insert one record into the database table
        /// </summary>
        /// <typeparam name="TEntity">Entity to add into</typeparam>
        /// <param name="record">The record to be added</param>
        public void Insert<TEntity>(TEntity record) where TEntity : class
            {
                Context.Set<TEntity>().Add(record);
            }



            /// <summary>
            /// Asynchronously, Insert one record into the database table
            /// </summary>
            /// <typeparam name="TEntity">Entity to add into</typeparam>
            /// <param name="record">The record to be added</param>
            public Task InsertAsync<TEntity>(TEntity record) where TEntity : class
            {
                return Task.Factory.StartNew(() => Insert(record));
            }



            /// <summary>
            /// Insert a range of reords in a table
            /// </summary>
            /// <typeparam name="TEntity">Entity to insert into</typeparam>
            /// <param name="records">Records to be inserted</param>
            public void InsertRange<TEntity>(List<TEntity> records) where TEntity : class
            {
                Context.Set<TEntity>().AddRange(records);
            }



            /// <summary>
            /// Asynchronously, Insert a range of reords in a table
            /// </summary>
            /// <typeparam name="TEntity">Entity to insert into</typeparam>
            /// <param name="records">Records to be inserted</param>
            public Task InsertRangeAsync<TEntity>(List<TEntity> records) where TEntity : class
            {
                return Task.Factory.StartNew(() => InsertRange(records));
            }



            #endregion




        #region Delete

            /// <summary>
            /// Delete One record from a database table
            /// </summary>
            /// <typeparam name="TEntity">Entity to remove from</typeparam>
            /// <param name="record">The record to be removed</param>
            public void Delete<TEntity>(TEntity record) where TEntity : class
            {
                this.Context.Set<TEntity>().Remove(record);
            }



            /// <summary>
            /// Asynchronously, Delete One record from a database table
            /// </summary>
            /// <typeparam name="TEntity">Entity to remove from</typeparam>
            /// <param name="record">The record to be removed</param>
            public Task DeleteAsync<TEntity>(TEntity record) where TEntity : class
            {
                return Task.Factory.StartNew(() => Delete(record));
            }


            #endregion




            /// <summary>
            /// Save the repository changes
            /// </summary>
            public void Save()
            {
                Context.SaveChanges();
            }


        #endregion




        #region Disposing


            #region Properties

            private bool _disposed { get; set; } = false;

            #endregion


            private void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        Context.Dispose();
                    }
                }

                _disposed = true;
            }



            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }


        #endregion


    }
}