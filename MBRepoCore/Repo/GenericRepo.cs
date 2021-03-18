using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MBRepoCore.Enums;
using MBRepoCore.Factories;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo
{
    /// <summary>
    ///Full generic repository
    /// </summary>
    /// <typeparam name="TContext">The <b><see cref="DbContext"/></b> type</typeparam>
    public sealed class GenericRepo<TContext> : IGenericRepo<TContext>, IRepoProperties, IDisposable where TContext : DbContext
    {
        #region properties

        /// <inheritdoc />
        public DbContext Context { get; } = null;


        /// <inheritdoc />
        public bool LazyLoaded
        {
            get => Context.ChangeTracker.LazyLoadingEnabled;
            set => Context.ChangeTracker.LazyLoadingEnabled = value;
        }

        #endregion


        #region Construcors

        /// <summary>
        /// Use this constructor if you can create a <b><see cref="TContext"/></b> object without any parameters
        /// </summary>
        /// <param name="lazyLoaded">Determine if lazy loading whether active or not</param>
        public GenericRepo(bool lazyLoaded)
        {
            Context = RepoDBContextFactory<TContext>.GetInstance();
            ConfigureLazyLoading(lazyLoaded);
        }


        /// <summary>
        /// Use this constructor if you already have a created <b><see cref="TContext"/></b> object  
        /// </summary>
        /// <param name="context">Object from <b><see cref="TContext"/></b> context</param>
        /// <param name="lazyLoaded">Determine if lazy loading whether active or not</param>
        public GenericRepo(TContext context, bool lazyLoaded)
        {
            Context = context;
            ConfigureLazyLoading(lazyLoaded);
        }


        ///  <summary>
        ///  This constructor can used inside any .Net Core application
        ///  </summary>
        ///  <param name="connectionString">
        ///  <para>The database connection string</para>
        /// <example>
        ///  <para><b>Example 1</b></para>
        ///  
        ///  <para>The connection string can be :</para>
        /// <code>Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;</code>
        ///  <para><b>Example 2</b></para>
        ///  <para>The connection string can be :</para>
        ///  <code>configuration.GetConnectionString("DB connection part in connectionStrings inside appSettings.json")</code>
        /// <para>The <see cref="connectionString"/> also can be a <b>MySQL</b> or <b>Oracle</b> connection string</para>
        ///  </example>
        ///  </param>
        ///  <param name="rdbmsProvider">The <b>RDBMS/<see cref="RdbmsProvider"/></b> to be configured with</param>
        ///  <param name="lazyLoaded">Determine if lazy loading whether active or not</param>
        public GenericRepo(string connectionString, RdbmsProvider rdbmsProvider, bool lazyLoaded)
        {
            Context = CreateAndConfigureDbContextInstanceOptions(connectionString, rdbmsProvider);
            ConfigureLazyLoading(lazyLoaded);
        }

        #endregion


        #region Repository private methods

        /// <summary>
        /// An intermediate that prepaire and configure <b><see cref="IDbContextInstanceOptions"/></b>
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <param name="rdbmsProvider">The <b>RDBMS/<see cref="RdbmsProvider"/></b> to be configured with</param>
        /// <returns></returns>
        private TContext CreateAndConfigureDbContextInstanceOptions(string        connectionString,
                                                                    RdbmsProvider rdbmsProvider)
        {
            IDbContextInstanceOptions dbContextInstanceOptions = new DbContextInstanceOptions()
                                                                 {
                                                                     OptionsBuilder =
                                                                         new DbContextOptionsBuilder<TContext>(),
                                                                     connectionString = connectionString,
                                                                     RdbmsProvider    = rdbmsProvider
                                                                 };
            return RepoDBContextFactory<TContext>.GetInstance(dbContextInstanceOptions);
        }


        /// <summary>
        /// Set the <b><see cref="TContext"/></b> lazy loading
        /// </summary>
        /// <param name="lazyLoaded">Determine if lazy loading whether active or not</param>
        private void ConfigureLazyLoading(bool lazyLoaded)
        {
            LazyLoaded                               = lazyLoaded;
            Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
        }

        #endregion


        #region Routines

        #region Select

        /// <inheritdoc />
        public List<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>().ToList();
        }

        /// <inheritdoc />
        public List<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] expressions)
            where TEntity : class
        {
            var result = new List<TEntity>();

            foreach (var expression in expressions)
            {
                result = Context.Set<TEntity>().Include(expression).ToList();
            }


            return result;
        }


        /// <summary>
        /// Asynchronously, Get All <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <returns></returns>
        public Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            return Task.Factory.StartNew(() => GetAll<TEntity>().ToList());
        }


        /// <inheritdoc />
        public TEntity GetOne<TEntity>(object pkValue) where TEntity : class
        {
            return Context.Set<TEntity>().Find(pkValue);
        }


        /// <summary>
        /// Asynchronously, Get One <b><see cref="TEntity"/></b> record, based on the primary key value
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="pkValue">The primary key value</param>
        /// <returns></returns>
        public Task<TEntity> GetOneAsync<TEntity>(object pkValue) where TEntity : class
        {
            return Task.Factory.StartNew(() => GetOne<TEntity>(pkValue));
        }

        #endregion


        #region Contains

        /// <inheritdoc />
        public bool Contains<TEntity>(TEntity obj) where TEntity : class
        {
            return Context.Set<TEntity>().AsEnumerable().Contains(obj);
        }


        /// <summary>
        /// Asynchronously Check if <b><see cref="TEntity"/></b> contains an object
        /// </summary>
        /// <typeparam name="TEntity">Entity to be look in</typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns></returns>
        public Task<bool> ContainsAsync<TEntity>(TEntity obj) where TEntity : class
        {
            return Task.Factory.StartNew(() => Contains<TEntity>(obj));
        }


        /// <inheritdoc />
        public bool Contains<TEntity, TEntityComparer>(TEntity obj)
            where TEntity : class
            where TEntityComparer : IEqualityComparer<TEntity>, new()
        {
            return Context.Set<TEntity>().AsEnumerable()
                          .Contains(obj, new TEntityComparer() as IEqualityComparer<TEntity>);
        }


        /// <summary>
        ///  Asynchronously Check if <b><see cref="TEntity"/></b> contains an object based on a custom <b><see cref="IEqualityComparer{T}"/></b>
        /// </summary>
        /// <typeparam name="TEntity">Entity to be look in</typeparam>
        /// <typeparam name="TEntityComparer">The custom TEntity <b><see cref="IEqualityComparer{T}"/></b></typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns></returns>
        public Task<bool> ContainsAsync<TEntity, TEntityComparer>(TEntity obj)
            where TEntity : class
            where TEntityComparer : IEqualityComparer<TEntity>, new()
        {
            return Task.Factory.StartNew(() => Contains<TEntity, TEntityComparer>(obj));
        }

        #endregion


        #region Add

        /// <inheritdoc />
        public void AddOne<TEntity>(TEntity record) where TEntity : class
        {
            Context.Set<TEntity>().Add(record);
        }


        /// <summary>
        /// Asynchronously, Add one <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <typeparam name="TEntity">Entity to add into</typeparam>
        /// <param name="record">The record to be added</param>
        public Task AddOneAsync<TEntity>(TEntity record) where TEntity : class
        {
            return Task.Factory.StartNew(() => AddOne(record));
        }


        /// <inheritdoc />
        public void AddMany<TEntity>(List<TEntity> records) where TEntity : class
        {
            Context.Set<TEntity>().AddRange(records);
        }


        /// <summary>
        /// Asynchronously, Add a range of reords in a table
        /// </summary>
        /// <typeparam name="TEntity">Entity to insert into</typeparam>
        /// <param name="records">Records to be inserted</param>
        public Task AddManyAsync<TEntity>(List<TEntity> records) where TEntity : class
        {
            return Task.Factory.StartNew(() => AddMany(records));
        }

        #endregion


        #region Remove

        /// <inheritdoc />
        public void RemoveOne<TEntity>(TEntity record) where TEntity : class
        {
            this.Context.Set<TEntity>().Remove(record);
        }


        /// <summary>
        /// Asynchronously, Remove One <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <typeparam name="TEntity">Entity to delete from</typeparam>
        /// <param name="record">The record to be removed</param>
        public Task RemoveOneAsync<TEntity>(TEntity record) where TEntity : class
        {
            return Task.Factory.StartNew(() => RemoveOne(record));
        }


        /// <inheritdoc />
        public void RemoveMany<TEntity>(List<TEntity> records) where TEntity : class
        {
            this.Context.Set<TEntity>().RemoveRange(records);
        }

        /// <summary>
        /// Asynchronously, delete a range of <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="records"></param>
        /// <returns></returns>
        public Task RemoveManyAsync<TEntity>(List<TEntity> records) where TEntity : class
        {
            return Task.Factory.StartNew(() => RemoveMany(records));
        }

        #endregion


        #region Filter

        /// <inheritdoc />
        public List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class
        {
            IQueryable<TEntity> entity = Context.Set<TEntity>();

            return entity.Where(filterExpression).ToList();
        }


        /// <summary>
        /// Asynchronously, <inheritdoc cref="Filter{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">The entity to be filtered</typeparam>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> FilterAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class
        {
            return Task<IEnumerable<TEntity>>.Factory.StartNew(() => Filter<TEntity>(filterExpression));
        }


        /// <inheritdoc />
        public List<TEntity> FilterAndOrder<TEntity>(Expression<Func<TEntity, bool>> filterExpression,
                                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc)
            where TEntity : class
        {
            IQueryable<TEntity> entity = Context.Set<TEntity>();
            entity = entity.Where(filterExpression);
            entity = orderingFunc(entity);

            return entity.ToList();
        }


        /// <summary>
        /// Asynchronously, <inheritdoc cref="FilterAndOrder{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">The entity to be filtered and ordered</typeparam>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        /// <param name="orderingFunc">The <b><see cref="IOrderedQueryable{T}"/></b> ordering expression</param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> FilterWithOrderAsync<TEntity>(
            Expression<Func<TEntity, bool>>                       filterExpression,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc) where TEntity : class
        {
            return Task<IEnumerable<TEntity>>.Factory.StartNew(() => FilterAndOrder(filterExpression, orderingFunc));
        }

        #endregion


        #region Preview features

        /// <summary>
        /// Get Many records from <b><see cref="TEntity"/></b> based on a property value
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