using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo
{
    class ClassicRepo<TEntity>:IClassicRepo<TEntity>,IRepoProperties where TEntity : class
    {

        #region Properties

        /// <inheritdoc />
        public DbContext Context    { get; }

        /// <inheritdoc />
        public bool      LazyLoaded { get; set; }

        #endregion



        #region Constructors

        public ClassicRepo(DbContext context, bool lazyLoaded)
        {
            Context = context;
            ConfigureLazyLoading(lazyLoaded);
        }

        #endregion



        #region Repository private methods


        /// <summary>
        /// Set the <b><see cref="Context"/></b> lazy loading
        /// </summary>
        /// <param name="lazyLoaded">Determine if lazy loading whether active or not</param>
        private void ConfigureLazyLoading(bool lazyLoaded)
        {
            Context.ChangeTracker.LazyLoadingEnabled = LazyLoaded;
            LazyLoaded                               = lazyLoaded;
        }

        #endregion



        #region Routins



        #region Get

        /// <inheritdoc />
        public List<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public TEntity GetOne(object pkValue)
        {
            throw new NotImplementedException();
        }

        #endregion



        #region Add

        /// <inheritdoc />
        public void AddOne(TEntity record)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void AddMany(List<TEntity> records)
        {
            throw new NotImplementedException();
        }


        #endregion



        #region Contains


        /// <inheritdoc />
        public bool Contains(TEntity obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Contains<TEntityComparer>(TEntity obj) where TEntityComparer : IEqualityComparer<TEntity>, new()
        {
            throw new NotImplementedException();
        }

        #endregion



        #region Remove

        /// <inheritdoc />
        public void RemoveOne(TEntity record)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void RemoveMany(List<TEntity> records)
        {
            throw new NotImplementedException();
        }

        #endregion



        #region Filter

        /// <inheritdoc />
        public List<TEntity> Filter(Expression<Func<TEntity, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public List<TEntity> FilterAndOrder(Expression<Func<TEntity, bool>> filterExpression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc)
        {
            throw new NotImplementedException();
        }

        #endregion


        #endregion


    }
}
