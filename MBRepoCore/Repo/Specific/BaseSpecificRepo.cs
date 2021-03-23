using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MBRepoCore.Repo.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace MBRepoCore.Repo.Specific
{
    /// <summary>
    ///Classic repository with <see cref="IBasicRepository"/> and <see cref="IAdvancedRepository"/> features, Can be the base class for your <b><see cref="TEntity"/></b> repository
    /// </summary>
    /// <typeparam name="TEntity">The entity to create repository for</typeparam>
    public abstract class BaseSpecificRepo<TEntity> : ISpecificRepo<TEntity>, IRepoProperties where TEntity : class
    {

        #region Properties

        /// <inheritdoc />
        public virtual DbContext Context { get; }

        /// <inheritdoc />
        public virtual bool LazyLoaded { get; set; }

        #endregion

        #region Contructor

        protected BaseSpecificRepo(DbContext context,
                                   bool      lazyLoaded)
        {
            Context = context;
            ConfigureLazyLoading(lazyLoaded);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Set the <b><see cref="Context"/></b> lazy loading
        /// </summary>
        /// <param name="lazyLoaded">Determine if lazy loading whether active or not</param>
        protected virtual void ConfigureLazyLoading(bool lazyLoaded)
        {
            Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
            LazyLoaded                               = lazyLoaded;
        }

        #endregion

        #region Routins

        #region Get

        /// <inheritdoc />
        public virtual List<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        /// <inheritdoc />
        public virtual List<TEntity> GetMany(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Context.Set<TEntity>().Where(filterExpression).ToList();
        }

        /// <inheritdoc />
        public virtual List<TEntity> GetMany(Expression<Func<TEntity, bool>>            filterExpression,
                                             params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded)
        {
            var result = Context.Set<TEntity>().Where(filterExpression).AsQueryable();

            result = relatedEntitiesToBeLoaded.Aggregate(result, (current,
                                                                  expression) => current.Include(expression));

            return result.ToList();
        }

        /// <inheritdoc />
        public virtual List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded)
        {
            var result = Context.Set<TEntity>().AsQueryable();

            result = relatedEntitiesToBeLoaded.Aggregate(result, (current,
                                                                  expression) => current.Include(expression));

            return result.ToList();
        }

        /// <inheritdoc />
        public virtual TEntity GetOne(object pkValue)
        {
            return Context.Set<TEntity>().Find(pkValue);
        }

        #endregion

        #region Add

        /// <inheritdoc />
        public virtual void AddOne(TEntity record)
        {
            Context.Set<TEntity>().Add(record);
        }

        /// <inheritdoc />
        public virtual void AddMany(List<TEntity> records)
        {
            Context.Set<TEntity>().AddRange(records);
        }

        #endregion

        #region Update

        /// <inheritdoc />
        public virtual void UpdateOne(TEntity record)
        {
            var entity = Context.Set<TEntity>();
            entity.Attach(record);
            Context.Entry(record).State = EntityState.Modified;
        }

        /// <inheritdoc />
        public virtual void UpdateMany(Expression<Func<TEntity, bool>> filterExpression,
                                       Action<TEntity>                 updateAction)
        {
            // Get the records to be updated depending on the filter expression
            var recordsToBeUpdated = Context.Set<TEntity>().Where(filterExpression).ToList();

            // Update the selected records
            recordsToBeUpdated.ForEach(updateAction);
        }

        #endregion

        #region Contains

        /// <inheritdoc />
        public virtual bool Contains(TEntity obj)
        {
            return Context.Set<TEntity>().AsEnumerable().Contains(obj);
        }

        /// <inheritdoc />
        public virtual bool Contains<TEntityComparer>(TEntity obj)
            where TEntityComparer : IEqualityComparer<TEntity>, new()
        {
            return Context.Set<TEntity>()
                          .AsEnumerable()
                          .Contains(obj, new TEntityComparer() as IEqualityComparer<TEntity>);
        }

        #endregion

        #region Remove

        /// <inheritdoc />
        public virtual void RemoveOne(TEntity record)
        {
            this.Context.Set<TEntity>().Remove(record);
        }

        /// <inheritdoc />
        public virtual void RemoveMany(List<TEntity> records)
        {
            this.Context.Set<TEntity>().RemoveRange(records);
        }

        #endregion

        #region Filter

        /// <inheritdoc />
        public virtual List<TEntity> Filter(Expression<Func<TEntity, bool>> filterExpression)
        {
            IQueryable<TEntity> entity = Context.Set<TEntity>();

            return entity.Where(filterExpression).ToList();
        }

        /// <inheritdoc />
        public virtual List<TEntity> FilterAndOrder(Expression<Func<TEntity, bool>> filterExpression,
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc)
        {
            IQueryable<TEntity> entity = Context.Set<TEntity>();
            entity = entity.Where(filterExpression);
            entity = orderingFunc(entity);

            return entity.ToList();
        }

        #endregion

        #endregion

    }
}