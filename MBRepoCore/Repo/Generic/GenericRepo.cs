using MBRepoCore.Enums;
using MBRepoCore.Factories;
using MBRepoCore.Repo.Abstractions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MBRepoCore.Extensions;
using MBRepoCore.Interfaces;


namespace MBRepoCore.Repo.Generic
{
    /// <summary>
    ///Full generic repository
    /// </summary>
    /// <typeparam name="TContext">The <b><see cref="DbContext"/></b> type</typeparam>
    public class GenericRepo<TContext> : IGenericRepo<TContext> , IRepoProperties , IDisposable where TContext : DbContext
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
        public GenericRepo( bool lazyLoaded = false )
        {
            Context = RepoDbContextFactory<TContext>.GetInstance();
            ConfigureLazyLoading( lazyLoaded );
        }


        /// <summary>
        /// Use this constructor if you already have a created <b><see cref="TContext"/></b> object  
        /// </summary>
        /// <param name="context">Object from <b><see cref="TContext"/></b> context</param>
        /// <param name="lazyLoaded">Determine if lazy loading whether active or not</param>
        public GenericRepo( TContext context , bool lazyLoaded = false )
        {
            Context = context;
            ConfigureLazyLoading( lazyLoaded );
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
        public GenericRepo( string connectionString , RdbmsProvider rdbmsProvider , bool lazyLoaded = false )
        {
            Context = CreateAndConfigureDbContextInstanceOptions( connectionString , rdbmsProvider );
            ConfigureLazyLoading( lazyLoaded );
        }

        #endregion


        #region Repository private methods

        /// <summary>
        /// An intermediate that prepaire and configure <b><see cref="IDbContextInstanceOptions"/></b>
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <param name="rdbmsProvider">The <b>RDBMS/<see cref="RdbmsProvider"/></b> to be configured with</param>
        /// <returns></returns>
        private TContext CreateAndConfigureDbContextInstanceOptions( string  connectionString , RdbmsProvider rdbmsProvider )
        {
            IDbContextInstanceOptions dbContextInstanceOptions = new DbContextInstanceOptions()
                                                                 {
                                                                     OptionsBuilder =
                                                                         new DbContextOptionsBuilder<TContext>() ,
                                                                     ConnectionString = connectionString ,
                                                                     RdbmsProvider    = rdbmsProvider
                                                                 };

            return RepoDbContextFactory<TContext>.GetInstance( dbContextInstanceOptions );
        }


        /// <summary>
        /// Set the <b><see cref="TContext"/></b> lazy loading
        /// </summary>
        /// <param name="lazyLoaded">Determine if lazy loading whether active or not</param>
        private void ConfigureLazyLoading( bool lazyLoaded )
        {
            LazyLoaded                               = lazyLoaded;
            Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
        }

        #endregion


        #region Routines

        #region Get

        /// <inheritdoc />
        public List<TEntity> Get<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>().ToList();
        }

        /// <inheritdoc />
        public Task<List<TEntity>> GetAsync<TEntity>() where TEntity : class
        {
            return Task.Factory.StartNew( () => Get<TEntity>() );
        }


        /// <inheritdoc />
        public List<TEntity> Get<TEntity>( params Expression<Func<TEntity , object>>[] relatedEntitiesToBeLoaded )
            where TEntity : class
        {
            var result = Context.Set<TEntity>().AsQueryable();

            result = relatedEntitiesToBeLoaded.Aggregate( result ,
                                                          ( current ,
                                                            expression ) => current.Include( expression ) );


            return result.ToList();
        }

        /// <inheritdoc />
        public Task<List<TEntity>> GetAsync<TEntity>(params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class
        {
            return Task.Factory.StartNew(() => Get<TEntity>(relatedEntitiesToBeLoaded));
        }


        /// <inheritdoc />
        public List<TEntity> Get<TEntity>( Expression<Func<TEntity , bool>> filterExpression ) where TEntity : class
        {
            return Context.Set<TEntity>().Where( filterExpression ).ToList();
        }

        /// <inheritdoc />
        public Task<List<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class
        {
            return Task.Factory.StartNew(() => Get<TEntity>(filterExpression));
        }


        /// <inheritdoc />
        public List<TEntity> Get<TEntity>( Expression<Func<TEntity , bool>> filterExpression , params Expression<Func<TEntity , object>>[] relatedEntitiesToBeLoaded )
            where TEntity : class
        {
            var result = Context.Set<TEntity>().Where( filterExpression ).AsQueryable();

            result = relatedEntitiesToBeLoaded.Aggregate( result ,
                                                          ( current ,
                                                            expression ) => current.Include( expression ) );

            return result.ToList();
        }

        /// <inheritdoc />
        public Task<List<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class
        {
            return Task.Factory.StartNew(() => Get<TEntity>(filterExpression, relatedEntitiesToBeLoaded));
        }


        /// <inheritdoc />
        public TEntity GetById<TEntity>( object pkValue ) where TEntity : class
        {
            return Context.Set<TEntity>().Find( pkValue );
        }

        /// <inheritdoc />
        public Task<TEntity> GetByIdAsync<TEntity>(object pkValue) where TEntity : class
        {
            return Task.Factory.StartNew( () => GetById<TEntity>( pkValue ) );
        }


        /// <inheritdoc />
        public TEntity GetById<TEntity>( object pkValue , params Expression<Func<TEntity , object>>[] relatedEntitiesToBeLoaded ) where TEntity : class
        {
            // Change DbContext tracking behavior to track all entities
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            // Get one object using primary key
            var resultObject = Context.Set<TEntity>().Find( pkValue );

            // Load all selected objects from selected entities
            foreach ( var entityToLoad in relatedEntitiesToBeLoaded )
            {
                Context.Entry( resultObject ).Reference( entityToLoad.GetPropertyAccess().Name ).Load();
            }

            return resultObject;
        }

        /// <inheritdoc />
        public Task<TEntity> GetByIdAsync<TEntity>(object pkValue, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class
        {
            return Task.Factory.StartNew(() => GetById<TEntity>(pkValue, relatedEntitiesToBeLoaded));
        }



        #endregion


        #region Add

        /// <inheritdoc />
        public void Add<TEntity>( TEntity record ) where TEntity : class
        {
            Context.Set<TEntity>().Add( record );
        }

        /// <inheritdoc />
        public Task AddAsync<TEntity>(TEntity record) where TEntity : class
        {
            return Task.Factory.StartNew( () => Add<TEntity>( record ) );
        }


        /// <inheritdoc />
        public void Add<TEntity>( List<TEntity> records ) where TEntity : class
        {
            Context.Set<TEntity>().AddRange( records );
        }

        /// <inheritdoc />
        public Task AddAsync<TEntity>( List<TEntity> records ) where TEntity : class
        {
            return Task.Factory.StartNew( () => Add( records ) );
        }

        #endregion


        #region Update

        /// <inheritdoc />
        public void Update<TEntity>( TEntity record ) where TEntity : class
        {
            var entity = Context.Set<TEntity>();
            entity.Attach( record );
            Context.Entry( record ).State = EntityState.Modified;
        }

        /// <inheritdoc />
        public Task UpdateAsync<TEntity>(TEntity record) where TEntity : class
        {
            return Task.Factory.StartNew( () => Update<TEntity>( record ) );
        }


        /// <inheritdoc />
        public void Update<TEntity>( Expression<Func<TEntity , bool>> filterExpression , Action<TEntity> updateAction ) where TEntity : class
        {
            // Get the records to be updated depending on the filter expression
            var recordsToBeUpdated = Context.Set<TEntity>().Where( filterExpression ).ToList();

            // Update the selected records
            recordsToBeUpdated.ForEach( updateAction );
        }

        /// <inheritdoc />
        public Task UpdateAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression, Action<TEntity> updateAction) where TEntity : class
        {
            return Task.Factory.StartNew(() => Update<TEntity>(filterExpression, updateAction));
        }


        /// <inheritdoc />
        public void UpdateExcept<TEntity>( TEntity record , Expression<Func<TEntity , object>> propertiesToBeSkipped ) where TEntity : class
        {
            var entity = Context.Set<TEntity>();
            entity.Attach( record );
            Context.Entry( record ).State = EntityState.Modified;

            foreach ( var property in propertiesToBeSkipped.GetMemberAccessList() )
            {
                Context.Entry( record ).Property( property.Name ).IsModified = false;
            }
        }

        /// <inheritdoc />
        public Task UpdateExceptAsync<TEntity>( TEntity record , Expression<Func<TEntity , object>> propertiesToBeExcluded ) where TEntity : class
        {
            return Task.Factory.StartNew( () => UpdateExcept( record , propertiesToBeExcluded ) );
        }


        /// <inheritdoc />
        public void UpdateExcept<TEntity , TSkippable>( TEntity record ) where TEntity : class where TSkippable : ISkippable<TEntity> , new()
        {
            var entity = Context.Set<TEntity>();
            entity.Attach( record );
            Context.Entry( record ).State = EntityState.Modified;

            var propertiesToBeExcluded = new TSkippable().GetSkiped();

            foreach ( var property in  propertiesToBeExcluded.GetPropertyAccessList())
            {
                Context.Entry( record ).Property( property.Name ).IsModified = false;
            }
        }

        /// <inheritdoc />
        public Task UpdateExceptAsync<TEntity , TSkippable>( TEntity record ) where TEntity : class where TSkippable : ISkippable<TEntity> , new()
        {
            return Task.Factory.StartNew( () => UpdateExcept<TEntity , TSkippable>( record ) );
        }


        /// <inheritdoc />
        public void UpdateExcept<TEntity , TSkippable>( TEntity record , Expression<Func<TEntity , object>> otherPropertiesToBeSkipped ) where TEntity : class where TSkippable : ISkippable<TEntity> , new()
        {
            var entity = Context.Set<TEntity>();
            entity.Attach( record );
            Context.Entry( record ).State = EntityState.Modified;

            var propertiesToBeSkiped = new List<Expression<Func<TEntity , object>>>
                                       {
                                           new TSkippable().GetSkiped() , otherPropertiesToBeSkipped
                                       };

            foreach (var property in propertiesToBeSkiped.GetMemberAccess())
            {
                Context.Entry(record).Property(property.Name).IsModified = false;
            }
        }


        /// <inheritdoc />
        public Task UpdateExceptAsync<TEntity , TSkippable>( TEntity record , Expression<Func<TEntity , object>> propertiesToBeExcluded ) where TEntity : class where TSkippable : ISkippable<TEntity> , new()
        {
            return Task.Factory.StartNew( () => UpdateExcept<TEntity , TSkippable>( record , propertiesToBeExcluded ) );
        }

        #endregion


        #region Remove

        /// <inheritdoc />
        public void Remove<TEntity>( TEntity record ) where TEntity : class
        {
            this.Context.Set<TEntity>().Remove( record );
        }

        /// <inheritdoc />
        public Task RemoveAsync<TEntity>( TEntity record ) where TEntity : class
        {
            return Task.Factory.StartNew( () => Remove( record ) );
        }


        /// <inheritdoc />
        public void Remove<TEntity>( List<TEntity> records ) where TEntity : class
        {
            this.Context.Set<TEntity>().RemoveRange( records );
        }

        /// <inheritdoc />
        public Task RemoveAsync<TEntity>( List<TEntity> records ) where TEntity : class
        {
            return Task.Factory.StartNew( () => Remove( records ) );
        }

        #endregion


        #region Is Exist

        /// <inheritdoc />
        public bool IsExist<TEntity>( object pkValue ) where TEntity : class
        {
            var result = Context.Set<TEntity>().Find( pkValue );

            return result != null;
        }

        /// <inheritdoc />
        public Task<bool> IsExistAsync<TEntity>(object pkValue) where TEntity : class
        {
            return Task.Factory.StartNew( () => IsExist<TEntity>( pkValue ) );
        }

        /// <inheritdoc />
        public bool IsExist<TEntity>( Expression<Func<TEntity , bool>> selectExpression ) where TEntity : class
        {
            var result = Context.Set<TEntity>().FirstOrDefault( selectExpression );

            return result != null;
        }

        /// <inheritdoc />
        public Task<bool> IsExistAsync<TEntity>(Expression<Func<TEntity, bool>> selectExpression) where TEntity : class
        {
            return Task.Factory.StartNew( () => IsExist( selectExpression ) );
        }

        #endregion
        
        
        #region Contains

        /// <inheritdoc />
        public bool Contains<TEntity>( TEntity obj ) where TEntity : class
        {
            return Context.Set<TEntity>().AsEnumerable().Contains( obj );
        }

        /// <inheritdoc />
        public Task<bool> ContainsAsync<TEntity>( TEntity obj ) where TEntity : class
        {
            return Task.Factory.StartNew( () => Contains<TEntity>( obj ) );
        }


        /// <inheritdoc />
        public bool Contains<TEntity , TEntityComparer>( TEntity obj ) where TEntity : class where TEntityComparer : IEqualityComparer<TEntity> , new()
        {
            return Context.Set<TEntity>()
                          .AsEnumerable()
                          .Contains( obj , new TEntityComparer() as IEqualityComparer<TEntity> );
        }

        /// <inheritdoc />
        public Task<bool> ContainsAsync<TEntity , TEntityComparer>( TEntity obj ) where TEntity : class where TEntityComparer : IEqualityComparer<TEntity> , new()
        {
            return Task.Factory.StartNew( () => Contains<TEntity , TEntityComparer>( obj ) );
        }

        #endregion


        #region Filter

        /// <inheritdoc />
        public List<TEntity> Filter<TEntity>( Expression<Func<TEntity , bool>> filterExpression ) where TEntity : class
        {
            IQueryable<TEntity> entity = Context.Set<TEntity>();

            return entity.Where( filterExpression ).ToList();
        }

        /// <inheritdoc />
        public Task<List<TEntity>> FilterAsync<TEntity>( Expression<Func<TEntity , bool>> filterExpression ) where TEntity : class
        {
            return Task<List<TEntity>>.Factory.StartNew( () => Filter<TEntity>( filterExpression ) );
        }


        /// <inheritdoc />
        public List<TEntity> FilterAndOrder<TEntity>( Expression<Func<TEntity , bool>> filterExpression , Func<IQueryable<TEntity> , IOrderedQueryable<TEntity>> orderingFunc ) where TEntity : class
        {
            IQueryable<TEntity> entity = Context.Set<TEntity>();
            entity = entity.Where( filterExpression );
            entity = orderingFunc( entity );

            return entity.ToList();
        }

        /// <inheritdoc />
        public Task<List<TEntity>> FilterAndOrderAsync<TEntity>( Expression<Func<TEntity , bool>> filterExpression , Func<IQueryable<TEntity> , IOrderedQueryable<TEntity>> orderingFunc ) where TEntity : class
        {
            return Task<List<TEntity>>.Factory.StartNew( () => FilterAndOrder( filterExpression , orderingFunc ) );
        }

        #endregion


        #endregion


        #region Disposing

        #region Properties

        private bool _disposed { get; set; } = false;

        #endregion


        private void Dispose( bool disposing )
        {
            if ( !_disposed )
            {
                if ( disposing )
                {
                    Context.Dispose();
                }
            }

            _disposed = true;
        }


        public void Dispose()
        {
            this.Dispose( true );
            GC.SuppressFinalize( this );
        }

        #endregion

    }
}