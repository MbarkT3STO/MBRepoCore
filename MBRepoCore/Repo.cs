using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore
{

    /// <typeparam name="TContext"></typeparam>
    public sealed class Repo<TContext> : IRepo<TContext>, IDisposable where TContext : DbContext, new()
    {

        


        #region properties

        /// <summary>
        /// Private DBContext property
        /// </summary>
        private DbContext _Context { get; } = null;


        /// <summary>
        /// Determine if Lazy Loading either activate or not
        /// </summary>
        private bool _LazyLoaded { get; set; }

        #endregion




        #region Construcors


        public Repo(bool LazyLoaded)
        {
            _Context                                  = new TContext();
            _LazyLoaded                               = LazyLoaded;
            _Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
        }
        public Repo(DbContext context,bool LazyLoaded)
        {
            _Context                                  = context;
            _LazyLoaded                               = LazyLoaded;
            _Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
        }
        public Repo(DbContextOptionsBuilder<TContext> optionsBuilder,bool LazyLoaded)
        {
            //_Context = new TContext(optionsBuilder);
            _Context = (TContext)Activator.CreateInstance(typeof(TContext), optionsBuilder) as TContext;
            _LazyLoaded = LazyLoaded;
            _Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
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
                return _Context.Set<TEntity>().ToList();
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
                return _Context.Set<TEntity>().Find(pkValue);
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
            /// <param name="prop">The property used in the condition</param>
            /// <param name="val">The value that will used in the search</param>
            /// <returns></returns>
            public IEnumerable<TEntity> GetMany<TEntity>(string prop, object val) where TEntity : class
            {
                return _Context.Set<TEntity>().AsEnumerable()
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
            return _Context.Set<TEntity>().AsEnumerable().Contains(obj);
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
            return _Context.Set<TEntity>().AsEnumerable().Contains(obj,new TEntityComparer() as IEqualityComparer<TEntity>);
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
                _Context.Set<TEntity>().Add(record);
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
                _Context.Set<TEntity>().AddRange(records);
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
                this._Context.Set<TEntity>().Remove(record);
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
                _Context.SaveChanges();
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
                        _Context.Dispose();
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