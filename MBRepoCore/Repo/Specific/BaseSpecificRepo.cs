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
using Microsoft.EntityFrameworkCore.Query;


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

        #region Set

        /// <inheritdoc />
        public DbSet<TEntity> Set() => Context.Set<TEntity>();

        #endregion

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
        public virtual List<TEntity> Get( params Expression<Func<TEntity , object>>[] andLoad )
        {
            var result = Context.Set<TEntity>().AsQueryable();

            result = andLoad.Aggregate( result ,
                                                          ( current ,
                                                            expression ) => current.Include( expression ) );

            return result.ToList();
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetAsync(params Expression<Func<TEntity, object>>[] andLoad)
        {
            return Task.Factory.StartNew( () => Get( andLoad ) );
        }


        /// <inheritdoc />
        public virtual List<TEntity> Get( Expression<Func<TEntity , bool>> @where )
        {
            return Context.Set<TEntity>().Where( @where ).ToList();
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> @where)
        {
            return Task.Factory.StartNew( () => Get( @where ) );
        }


        /// <inheritdoc />
        public virtual List<TEntity> Get( Expression<Func<TEntity , bool>> @where , params Expression<Func<TEntity , object>>[] andLoad )
        {
            var result = Context.Set<TEntity>().Where( @where ).AsQueryable();

            result = andLoad.Aggregate( result ,
                                                          ( current ,
                                                            expression ) => current.Include( expression ) );

            return result.ToList();
        }
        
        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] andLoad)
        {
            return Task.Factory.StartNew( () => Get( @where , andLoad ) );
        }


        /// <inheritdoc />
        public virtual List<TEntity> Get( Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include )
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

           query =  include( query );

            return query.ToList();
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return Task.Run( () => Get( include ) );
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
        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> @where, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            var query = Context.Set<TEntity>().Where( where );

            query = include( query );

            return query.ToList();
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> @where, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return Task.Run( () => Get( where , include ) );
        }


        /// <inheritdoc />
        public virtual TEntity GetById( object pkValue , params Expression<Func<TEntity , object>>[] andLoad )
        {
            // Change DbContext tracking behavior to track all entities
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            // Get one object using primary key
            var resultObject = Context.Set<TEntity>().Find( pkValue );

            // Load all selected objects from selected entities
            foreach ( var entityToLoad in andLoad )
            {
                Context.Entry( resultObject ).Reference( entityToLoad ).Load();
            }

            return resultObject;
        }

        /// <inheritdoc />
        public virtual Task<TEntity> GetByIdAsync(object pkValue, params Expression<Func<TEntity, object>>[] andLoad)
        {
            return Task.Factory.StartNew(() => GetById(pkValue, andLoad));
        }



        /// <inheritdoc />
        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> @where)
        {
            // Change DbContext tracking behavior to track all entities
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            var query = Context.Set<TEntity>().Where( where );

            return query.FirstOrDefault();
        }

        /// <inheritdoc />
        public Task<TEntity> GetFirstOrDefaultAsync( Expression<Func<TEntity , bool>> @where )
        {
            return Task.Run( () => GetFirstOrDefault( where ) );
        }

        /// <inheritdoc />
        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> @where, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            var query = Context.Set<TEntity>().Where(where);

            query = include( query );

            return query.FirstOrDefault();
        }

        /// <inheritdoc />
        public Task<TEntity> GetFirstOrDefaultAsync( Expression<Func<TEntity , bool>> @where , Func<IQueryable<TEntity> , IIncludableQueryable<TEntity , object>> include )
        {
            return Task.Run( () => GetFirstOrDefault( where , include ) );
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
        public virtual void Update( Expression<Func<TEntity , bool>> @where , Action<TEntity> @do )
        {
            // Get the records to be updated depending on the filter expression
            var recordsToBeUpdated = Context.Set<TEntity>().Where( @where ).ToList();

            // Update the selected records
            recordsToBeUpdated.ForEach( @do );
        }

        /// <inheritdoc />
        public virtual Task UpdateAsync(Expression<Func<TEntity, bool>> @where, Action<TEntity> @do)
        {
            return Task.Factory.StartNew( () => Update( @where , @do ) );

        }


        /// <inheritdoc />
        public virtual void UpdateExcept( TEntity record , Expression<Func<TEntity , object>> andSkip)
        {
            var entity = Context.Set<TEntity>();
            entity.Attach( record );
            Context.Entry( record ).State                                       = EntityState.Modified;

            foreach ( var property in andSkip.GetMemberAccessList() )
            {
                Context.Entry( record ).Property( property.Name ).IsModified = false;
            }
        }

        /// <inheritdoc />
        public virtual Task UpdateExceptAsync( TEntity record , Expression<Func<TEntity , object>> andSkip )
        {
            return Task.Factory.StartNew( () => UpdateExcept( record , andSkip ) );
        }


        /// <inheritdoc />
        public virtual void UpdateExcept<TSkippable>( TEntity record ) where TSkippable : ISkippable<TEntity>,new()
        {
            var entity = Context.Set<TEntity>();
            entity.Attach( record );
            Context.Entry( record ).State = EntityState.Modified;

            var propertiesToBeExcluded = new TSkippable().GetSkipped();

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
        public virtual void UpdateExcept<TSkippable>( TEntity record, Expression<Func<TEntity , object>> andSkip) where TSkippable : ISkippable<TEntity>,new()
        {
            var entity = Context.Set<TEntity>();
            entity.Attach( record );
            Context.Entry( record ).State = EntityState.Modified;

            var propertiesToBeSkiped = new List<Expression<Func<TEntity , object>>>
                                       {
                                           new TSkippable().GetSkipped() , andSkip
                                       };

            foreach (var property in propertiesToBeSkiped.GetMemberAccess())
            {
                Context.Entry(record).Property(property.Name).IsModified = false;
            }

        }

        /// <inheritdoc />
        public virtual Task UpdateExceptAsync<TSkippable>( TEntity record, Expression<Func<TEntity , object>> andSkip ) where TSkippable : ISkippable<TEntity>,new()
        {
            return Task.Factory.StartNew( () => UpdateExcept<TSkippable>( record , andSkip ) );
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
        public virtual bool IsExist( Expression<Func<TEntity , bool>> @where )
        {
            var result = Context.Set<TEntity>().FirstOrDefault( @where );

            return result != null;
        }

        /// <inheritdoc />
        public virtual Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> @where)
        {
            return Task.Factory.StartNew( () => IsExist( @where ) );
        }

        #endregion

        #region Get Partial

        /// <inheritdoc />
        public virtual List<TProperty> GetPartial<TProperty>(Expression<Func<TEntity, object>> @select)
        {
            List<TProperty> result = (from x in Context.Set<TEntity>().AsEnumerable() select (TProperty)x.GetType().GetProperty(@select.GetPropertyAccess().Name).GetValue(x, null)).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<TProperty>> GetPartialAsync<TProperty>(Expression<Func<TEntity, object>> @select)
        {
            return Task.Factory.StartNew(() => GetPartial<TProperty>(@select));
        }


        /// <inheritdoc />
        public virtual List<TProperty> GetPartial<TProperty>(Expression<Func<TEntity, object>> @select, Expression<Func<TEntity, bool>> @where)
        {
            List<TProperty> result = Context.Set<TEntity>().Where(@where).Select(x => (TProperty)x.GetType().GetProperty(@select.GetPropertyAccess().Name).GetValue(x, null)).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<TProperty>> GetPartialAsync<TProperty>(Expression<Func<TEntity, object>> @select, Expression<Func<TEntity, bool>> @where)
        {
            return Task.Factory.StartNew(() => GetPartial<TProperty>(@select, @where));
        }


        /// <inheritdoc />
        public virtual List<object> GetPartial( Func<TEntity, object> @select )
        {
            var result = Context.Set<TEntity>().Select(@select).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<object>> GetPartialAsync(Func<TEntity, object> @select)
        {
            return Task.Factory.StartNew(() => GetPartial(@select));
        }
        
        /// <inheritdoc />
        public virtual List<object> GetPartial( Func<TEntity, object> @select, Expression<Func<TEntity, bool>> @where )
        {
            var result = Context.Set<TEntity>().Where(@where).Select(@select).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<object>> GetPartialAsync(Func<TEntity, object> @select, Expression<Func<TEntity, bool>> @where)
        {
            return Task.Factory.StartNew(() => GetPartial(@select, @where));
        }


        /// <inheritdoc />
        public virtual List<TEntity> GetPartial( Func<TEntity, TEntity> @select )
        {
            var result = Context.Set<TEntity>().Select(@select).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetPartialAsync(Func<TEntity, TEntity> @select)
        {
            return Task.Factory.StartNew(() => GetPartial(@select));
        }
        
        /// <inheritdoc />
        public virtual List<TEntity> GetPartial( Func<TEntity, TEntity> @select, Expression<Func<TEntity, bool>> @where )
        {
            var result = Context.Set<TEntity>().Where(@where).Select(@select).ToList();
            return result;
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetPartialAsync(Func<TEntity, TEntity> @select, Expression<Func<TEntity, bool>> @where)
        {
            return Task.Factory.StartNew(() => GetPartial(@select, @where));
        }

        #endregion

        #region Get Where Not In

        /// <inheritdoc />
        public virtual List<TEntity> GetWhereNotIn<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifNotIn) where TNotIn : class
        {
            // Get data to look in
            var dataToCheckIn = Context.Set<TNotIn>().Select(ifNotIn).ToList();

            // Get data not in dataToCheckIn
            var result = Context.Set<TEntity>().AsEnumerable().Where(x => !dataToCheckIn.Contains(x.GetType().GetProperty(check.GetPropertyAccess().Name).GetValue(x, null))).ToList();

            return result;
        }
        
        /// <inheritdoc/>
        public virtual Task<List<TEntity>> GetWhereNotInAsync<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifNotIn) where TNotIn : class
        {
            return Task.Factory.StartNew(() => GetWhereNotIn<TNotIn>(check, ifNotIn));
        }


        /// <inheritdoc />
        public virtual List<TEntity> GetWhereNotIn<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifNotIn, Func<TEntity, bool> @where) where TNotIn : class
        {
            // Get data to look in
            var dataToCheckIn = Context.Set<TNotIn>().Select(ifNotIn).ToList();

            // Get filtered TEntity data not in dataToCheckIn
            var result = Context.Set<TEntity>().AsEnumerable().Where(@where).Where(x => !dataToCheckIn.Contains(x.GetType().GetProperty(check.GetPropertyAccess().Name).GetValue(x, null))).ToList();

            return result;
        }
        
        /// <inheritdoc/>
        public virtual Task<List<TEntity>> GetWhereNotInAsync<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifNotIn, Func<TEntity, bool> @where) where TNotIn : class
        {
            return Task.Factory.StartNew(() => GetWhereNotIn<TNotIn>(check, ifNotIn, @where));
        }


        /// <inheritdoc />
        public virtual List<TEntity> GetWhereNotIn<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifNotIn, Func<TNotIn, bool> @where) where TNotIn : class
        {
            // Get data to look in
            var dataToCheckIn = Context.Set<TNotIn>().Where(@where).Select(ifNotIn).ToList();

            // Get filtered TEntity data not in dataToCheckIn
            var result = Context.Set<TEntity>().AsEnumerable().Where(x => !dataToCheckIn.Contains(x.GetType().GetProperty(check.GetPropertyAccess().Name).GetValue(x, null))).ToList();

            return result;
        }

        /// <inheritdoc/>
        public virtual Task<List<TEntity>> GetWhereNotInAsync<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifNotIn, Func<TNotIn, bool> @where) where TNotIn : class
        {
            return Task.Factory.StartNew(() => GetWhereNotIn<TNotIn>(check, ifNotIn, @where));
        }


        /// <inheritdoc />
        public virtual List<TEntity> GetWhereNotIn<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifNotIn, Func<TEntity, bool> @where, Func<TNotIn, bool> andWhere) where TNotIn : class
        {
            // Get data to look in
            var dataToCheckIn = Context.Set<TNotIn>().Where(andWhere).Select(ifNotIn).ToList();

            // Get filtered TEntity data not in dataToCheckIn
            var result = Context.Set<TEntity>().AsEnumerable().Where(@where).Where(x => !dataToCheckIn.Contains(x.GetType().GetProperty(check.GetPropertyAccess().Name).GetValue(x, null))).ToList();

            return result;
        }
        
        /// <inheritdoc/>
        public virtual Task<List<TEntity>> GetWhereNotInAsync<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifNotIn, Func<TEntity, bool> @where, Func<TNotIn, bool> andWhere) where TNotIn : class
        {
            return Task.Factory.StartNew(() => GetWhereNotIn<TNotIn>(check, ifNotIn, @where, andWhere));
        }

        #endregion

        #region Get Where In

        /// <inheritdoc/>
        public virtual List<TEntity> GetWhereIn<Tin>(Expression<Func<TEntity, object>> check, Func<Tin, object> ifIn) where Tin : class
        {
            // Get data to look in
            var dataToLookIn = Context.Set<Tin>().Select(ifIn).ToList();

            // Get data in dataToLookIn
            var result = Context.Set<TEntity>().Where(x => dataToLookIn.Contains(x.GetType().GetProperty(check.GetPropertyAccess().Name).GetValue(x, null))).ToList();

            return result;
        }

        /// <inheritdoc/>
        public virtual Task<List<TEntity>> GetWhereInAsync<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifIn) where TNotIn : class
        {
           return Task.Factory.StartNew(() => GetWhereIn<TNotIn>(check, ifIn));
        }


        /// <inheritdoc/>
        public virtual List<TEntity> GetWhereIn<Tin>(Expression<Func<TEntity, object>> check, Func<Tin, object> ifIn, Func<TEntity, bool> @where) where Tin : class
        {
            // Ge data to look in
            var dataToLookIn = Context.Set<Tin>().Select(ifIn).ToList();

            // Get data in dataToLookIn
            var result = Context.Set<TEntity>().Where(@where).Where(x => dataToLookIn.Contains(x.GetType().GetProperty(check.GetPropertyAccess().Name).GetValue(x, null))).ToList();

            return result;
            }

        /// <inheritdoc/>
        public virtual Task<List<TEntity>> GetWhereInAsync<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifIn, Func<TEntity, bool> @where) where TNotIn : class
        {
            return Task.Factory.StartNew(() => GetWhereIn<TNotIn>(check, ifIn, @where));
        }


        /// <inheritdoc/>
        public virtual List<TEntity> GetWhereIn<Tin>(Expression<Func<TEntity, object>> check, Func<Tin, object> ifIn, Func<Tin, bool> @where) where Tin : class
        {
            // Ge data to look in
            var dataToLookIn = Context.Set<Tin>().Where(@where).Select(ifIn).ToList();

            // Get data in dataToLookIn
            var result = Context.Set<TEntity>().Where(x => dataToLookIn.Contains(x.GetType().GetProperty(check.GetPropertyAccess().Name).GetValue(x, null))).ToList();

            return result;
         }

        /// <inheritdoc/>
        public virtual Task<List<TEntity>> GetWhereInAsync<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifIn, Func<TNotIn, bool> @where) where TNotIn : class
        {
            return Task.Factory.StartNew(() => GetWhereIn<TNotIn>(check, ifIn, @where));
        }


        /// <inheritdoc/>
        public virtual List<TEntity> GetWhereIn<Tin> (Expression<Func<TEntity, object>> check, Func<Tin, object> ifIn, Func<TEntity, bool> @where, Func<Tin, bool> andWhere) where Tin : class
        {
            // Get data to look in
            var dataToLookIn = Context.Set<Tin>().Where(andWhere).Select(ifIn).ToList();

            // Get result
            var result = Context.Set<TEntity>().Where(@where).Where(x => dataToLookIn.Contains(x.GetType().GetProperty(check.GetPropertyAccess().Name).GetValue(x, null))).ToList();

            return result;
        }

        /// <inheritdoc/>
        public virtual Task<List<TEntity>> GetWhereInAsync<TNotIn>(Expression<Func<TEntity, object>> check, Func<TNotIn, object> ifIn, Func<TEntity, bool> @where, Func<TNotIn, bool> andWhere) where TNotIn : class
        {
            return Task.Factory.StartNew(() => GetWhereIn<TNotIn>(check, ifIn, @where, andWhere));
        }

        #endregion

        #endregion

    }
}