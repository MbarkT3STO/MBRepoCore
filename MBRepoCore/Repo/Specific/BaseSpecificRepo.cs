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


namespace MBRepoCore.Repo.Specific
{
    /// <summary>
    ///Classic repository with <see cref="IBasicRepository"/> and <see cref="IAdvancedRepository"/> features, Can be the base class for your <b><see cref="TEntity"/></b> repository
    /// </summary>
    /// <typeparam name="TEntity">The entity to create repository for</typeparam>
    public abstract class BaseSpecificRepo<TEntity> : ISpecificRepo<TEntity> , IRepoProperties where TEntity : class
    {

        #region Properties

        /// <inheritdoc />
        public virtual DbContext Context { get; }

        /// <inheritdoc />
        public virtual bool LazyLoaded { get; set; }

        #endregion

        #region Contructor

        protected BaseSpecificRepo( DbContext context , bool lazyLoaded = false )
        {
            Context = context;
            ConfigureLazyLoading( lazyLoaded );
        }

        #endregion

        #region private methods

        /// <summary>
        /// Set the <b><see cref="Context"/></b> lazy loading
        /// </summary>
        /// <param name="lazyLoaded">Determine if lazy loading whether active or not</param>
        protected virtual void ConfigureLazyLoading( bool lazyLoaded )
        {
            Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
            LazyLoaded                               = lazyLoaded;
        }

        #endregion

        #region Routins

        #region Get

        /// <inheritdoc />
        public virtual List<TEntity> Get()
        {
            return Context.Set<TEntity>().ToList();
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetAsync()
        {
            return Task.Factory.StartNew(() => Get());
        }


        /// <inheritdoc />
        public virtual List<TEntity> Get( params Expression<Func<TEntity , object>>[] relatedEntitiesToBeLoaded )
        {
            var result = Context.Set<TEntity>().AsQueryable();

            result = relatedEntitiesToBeLoaded.Aggregate( result ,
                                                          ( current ,
                                                            expression ) => current.Include( expression ) );

            return result.ToList();
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetAsync(params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded)
        {
            return Task.Factory.StartNew( () => Get( relatedEntitiesToBeLoaded ) );
        }


        /// <inheritdoc />
        public virtual List<TEntity> Get( Expression<Func<TEntity , bool>> filterExpression )
        {
            return Context.Set<TEntity>().Where( filterExpression ).ToList();
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Factory.StartNew( () => Get( filterExpression ) );
        }


        /// <inheritdoc />
        public virtual List<TEntity> Get( Expression<Func<TEntity , bool>> filterExpression , params Expression<Func<TEntity , object>>[] relatedEntitiesToBeLoaded )
        {
            var result = Context.Set<TEntity>().Where( filterExpression ).AsQueryable();

            result = relatedEntitiesToBeLoaded.Aggregate( result ,
                                                          ( current ,
                                                            expression ) => current.Include( expression ) );

            return result.ToList();
        }
        
        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filterExpression, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded)
        {
            return Task.Factory.StartNew( () => Get( filterExpression , relatedEntitiesToBeLoaded ) );
        }


        /// <inheritdoc />
        public virtual TEntity GetById( object pkValue )
        {
            return Context.Set<TEntity>().Find( pkValue );
        }

        /// <inheritdoc />
        public virtual Task<TEntity> GetByIdAsync(object pkValue)
        {
            return Task.Factory.StartNew( () => GetById( pkValue ) );
        }


        /// <inheritdoc />
        public virtual TEntity GetById( object pkValue , params Expression<Func<TEntity , object>>[] relatedEntitiesToBeLoaded )
        {
            // Change DbContext tracking behavior to track all entities
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            // Get one object using primary key
            var resultObject = Context.Set<TEntity>().Find( pkValue );

            // Load all selected objects from selected entities
            foreach ( var entityToLoad in relatedEntitiesToBeLoaded )
            {
                Context.Entry( resultObject ).Reference( entityToLoad ).Load();
            }

            return resultObject;
        }

        /// <inheritdoc />
        public virtual Task<TEntity> GetByIdAsync(object pkValue, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded)
        {
            return Task.Factory.StartNew(() => GetById(pkValue, relatedEntitiesToBeLoaded));
        }


        #endregion

        #region Add

        /// <inheritdoc />
        public virtual void Add( TEntity record )
        {
            Context.Set<TEntity>().Add( record );
        }

        /// <inheritdoc />
        public virtual Task AddAsync(TEntity record)
        {
            return Task.Factory.StartNew( () => Add( record ) );
        }


        /// <inheritdoc />
        public virtual void Add( List<TEntity> records )
        {
            Context.Set<TEntity>().AddRange( records );
        }

        /// <inheritdoc />
        public virtual Task AddAsync(List<TEntity> records)
        {
            return Task.Factory.StartNew( () => Add( records ) );
        }

        #endregion

        #region Update

        /// <inheritdoc />
        public virtual void Update( TEntity record )
        {
            var entity = Context.Set<TEntity>();
            entity.Attach( record );
            Context.Entry( record ).State = EntityState.Modified;
        }

        /// <inheritdoc />
        public virtual Task UpdateAsync(TEntity record)
        {
            return Task.Factory.StartNew( () => Update( record ) );
        }


        /// <inheritdoc />
        public virtual void Update( Expression<Func<TEntity , bool>> filterExpression , Action<TEntity> updateAction )
        {
            // Get the records to be updated depending on the filter expression
            var recordsToBeUpdated = Context.Set<TEntity>().Where( filterExpression ).ToList();

            // Update the selected records
            recordsToBeUpdated.ForEach( updateAction );
        }

        /// <inheritdoc />
        public virtual Task UpdateAsync(Expression<Func<TEntity, bool>> filterExpression, Action<TEntity> updateAction)
        {
            return Task.Factory.StartNew( () => Update( filterExpression , updateAction ) );

        }


        /// <inheritdoc />
        public virtual void UpdateExcept( TEntity record , Expression<Func<TEntity , object>> propertiesToBeExcluded)
        {
            var entity = Context.Set<TEntity>();
            entity.Attach( record );
            Context.Entry( record ).State                                       = EntityState.Modified;

            foreach ( var property in propertiesToBeExcluded.GetMemberAccessList() )
            {
                Context.Entry( record ).Property( property.Name ).IsModified = false;
            }
        }

        /// <inheritdoc />
        public virtual Task UpdateExceptAsync( TEntity record , Expression<Func<TEntity , object>> propertiesToBeSkipped )
        {
            return Task.Factory.StartNew( () => UpdateExcept( record , propertiesToBeSkipped ) );
        }


        /// <inheritdoc />
        public virtual void UpdateExcept<TSkippable>( TEntity record ) where TSkippable : ISkippable<TEntity>,new()
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
        public virtual Task UpdateExceptAsync<TSkippable>( TEntity record ) where TSkippable : ISkippable<TEntity>,new()
        {
            return Task.Factory.StartNew( () => UpdateExcept<TSkippable>( record ) );
        }


        /// <inheritdoc />
        public virtual void UpdateExcept<TSkippable>( TEntity record, Expression<Func<TEntity , object>> propertiesToBeExcluded) where TSkippable : ISkippable<TEntity>,new()
        {
            var entity = Context.Set<TEntity>();
            entity.Attach( record );
            Context.Entry( record ).State = EntityState.Modified;

            var propertiesToBeSkiped = new List<Expression<Func<TEntity , object>>>
                                       {
                                           new TSkippable().GetSkiped() , propertiesToBeExcluded
                                       };

            foreach (var property in propertiesToBeSkiped.GetMemberAccess())
            {
                Context.Entry(record).Property(property.Name).IsModified = false;
            }

        }

        /// <inheritdoc />
        public virtual Task UpdateExceptAsync<TSkippable>( TEntity record, Expression<Func<TEntity , object>> propertiesToBeSkipped ) where TSkippable : ISkippable<TEntity>,new()
        {
            return Task.Factory.StartNew( () => UpdateExcept<TSkippable>( record , propertiesToBeSkipped ) );
        }


        #endregion

        #region Contains

        /// <inheritdoc />
        public virtual bool Contains( TEntity obj )
        {
            return Context.Set<TEntity>().AsEnumerable().Contains( obj );
        }

        /// <inheritdoc />
        public virtual Task<bool> ContainsAsync(TEntity obj)
        {
            return Task.Factory.StartNew( () => Contains( obj ) );
        }


        /// <inheritdoc />
        public virtual bool Contains<TEntityComparer>( TEntity obj ) where TEntityComparer : IEqualityComparer<TEntity> , new()
        {
            return Context.Set<TEntity>()
                          .AsEnumerable()
                          .Contains( obj , new TEntityComparer() as IEqualityComparer<TEntity> );
        }

        /// <inheritdoc />
        public virtual Task<bool> ContainsAsync<TEntityComparer>(TEntity obj) where TEntityComparer : IEqualityComparer<TEntity>, new()
        {
            return Task.Factory.StartNew( () => Contains<TEntityComparer>( obj ) );
        }

        #endregion

        #region Remove

        /// <inheritdoc />
        public virtual void Remove( TEntity record )
        {
            this.Context.Set<TEntity>().Remove( record );
        }

        /// <inheritdoc />
        public virtual Task RemoveAsync(TEntity record)
        {
            return Task.Factory.StartNew( () => Remove( record ) );
        }


        /// <inheritdoc />
        public virtual void Remove( List<TEntity> records )
        {
            this.Context.Set<TEntity>().RemoveRange( records );
        }

        /// <inheritdoc />
        public virtual Task RemoveAsync(List<TEntity> records)
        {
            return Task.Factory.StartNew( () => Remove( records ) );
        }

        #endregion

        #region Filter

        /// <inheritdoc />
        public virtual List<TEntity> Filter( Expression<Func<TEntity , bool>> filterExpression )
        {
            IQueryable<TEntity> entity = Context.Set<TEntity>();

            return entity.Where( filterExpression ).ToList();
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task<List<TEntity>>.Factory.StartNew( () => Filter( filterExpression ) );
        }


        /// <inheritdoc />
        public virtual List<TEntity> FilterAndOrder( Expression<Func<TEntity , bool>> filterExpression , Func<IQueryable<TEntity> , IOrderedQueryable<TEntity>> orderingFunc )
        {
            IQueryable<TEntity> entity = Context.Set<TEntity>();
            entity = entity.Where( filterExpression );
            entity = orderingFunc( entity );

            return entity.ToList();
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> FilterAndOrderAsync(Expression<Func<TEntity, bool>> filterExpression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc)
        {
            return Task<List<TEntity>>.Factory.StartNew( () => FilterAndOrder( filterExpression , orderingFunc ) );
        }

        #endregion

        #region Is Exist

        /// <inheritdoc />
        public virtual bool IsExist(object pkValue)
        {
            var result = Context.Set<TEntity>().Find( pkValue );

            return result != null;
        }

        /// <inheritdoc />
        public virtual Task<bool> IsExistAsync(object pkValue)
        {
            return Task.Factory.StartNew( () => IsExist( pkValue ) );
        }


        /// <inheritdoc />
        public virtual bool IsExist( Expression<Func<TEntity , bool>> selectExpression )
        {
            var result = Context.Set<TEntity>().FirstOrDefault( selectExpression );

            return result != null;
        }

        /// <inheritdoc />
        public virtual Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selectExpression)
        {
            return Task.Factory.StartNew( () => IsExist( selectExpression ) );
        }

        #endregion

        #region Get Partial

        /// <inheritdoc />
        public virtual List<TProperty> GetPartial<TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected)
        {
            List<TProperty> result = (from x in Context.Set<TEntity>().AsEnumerable() select (TProperty)x.GetType().GetProperty(propertyToBeSelected.GetPropertyAccess().Name).GetValue(x, null)).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<TProperty>> GetPartialAsync<TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected)
        {
            return Task.Factory.StartNew(() => GetPartial<TProperty>(propertyToBeSelected));
        }


        /// <inheritdoc />
        public virtual List<TProperty> GetPartial<TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected, Expression<Func<TEntity, bool>> filterExpression)
        {
            List<TProperty> result = Context.Set<TEntity>().Where(filterExpression).Select(x => (TProperty)x.GetType().GetProperty(propertyToBeSelected.GetPropertyAccess().Name).GetValue(x, null)).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<TProperty>> GetPartialAsync<TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected, Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Factory.StartNew(() => GetPartial<TProperty>(propertyToBeSelected, filterExpression));
        }


        /// <inheritdoc />
        public virtual List<object> GetPartial( Func<TEntity, object> propertiesToBeSelected )
        {
            var result = Context.Set<TEntity>().Select(propertiesToBeSelected).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<object>> GetPartialAsync(Func<TEntity, object> propertiesToBeSelected)
        {
            return Task.Factory.StartNew(() => GetPartial(propertiesToBeSelected));
        }
        
        /// <inheritdoc />
        public virtual List<object> GetPartial( Func<TEntity, object> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression )
        {
            var result = Context.Set<TEntity>().Where(filterExpression).Select(propertiesToBeSelected).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<object>> GetPartialAsync(Func<TEntity, object> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Factory.StartNew(() => GetPartial(propertiesToBeSelected, filterExpression));
        }


        /// <inheritdoc />
        public virtual List<TEntity> GetPartial( Func<TEntity, TEntity> propertiesToBeSelected )
        {
            var result = Context.Set<TEntity>().Select(propertiesToBeSelected).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetPartialAsync(Func<TEntity, TEntity> propertiesToBeSelected)
        {
            return Task.Factory.StartNew(() => GetPartial(propertiesToBeSelected));
        }
        
        /// <inheritdoc />
        public virtual List<TEntity> GetPartial( Func<TEntity, TEntity> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression )
        {
            var result = Context.Set<TEntity>().Where(filterExpression).Select(propertiesToBeSelected).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetPartialAsync(Func<TEntity, TEntity> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Factory.StartNew(() => GetPartial(propertiesToBeSelected, filterExpression));
        }

        #endregion

        #endregion

    }
}