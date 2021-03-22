using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MBRepoCore.Repo.Abstractions
{
    /// <summary>
    /// Represent the advanced Generic repository
    /// </summary>
    public interface IAdvancedRepository
    {
        #region Get

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records, and load related selected entities records
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="expressions">Entities to be load data from (Should be the <see cref="TEntity"/>'s navigation properties)</param>
        /// <returns></returns>
        List<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] expressions) where TEntity : class;

        #endregion

        #region Filter

        /// <summary>
        /// Filter <b><see cref="TEntity"/></b> objects by a custom expression
        /// </summary>
        /// <typeparam name="TEntity">The entity to be filtered</typeparam>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        /// <returns></returns>
        List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;

        /// <summary>
        /// Filter and order <b><see cref="TEntity"/></b> objects by custom feltering and ordering expressions
        /// </summary>
        /// <typeparam name="TEntity">The entity to be filtered and ordered</typeparam>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        /// <param name="orderingFunc">The <b><see cref="IOrderedQueryable"/></b> ordering expression</param>
        /// <returns></returns>
        List<TEntity> FilterAndOrder<TEntity>(Expression<Func<TEntity, bool>>                       filterExpression,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc)
            where TEntity : class;

        #endregion
    }

    /// <summary>
    /// Represent the advanced specific repository
    /// </summary>
    /// <typeparam name="TEntity">Entity to create repository for</typeparam>
    public interface IAdvancedRepository<TEntity>
    {
        #region Get

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records, and load related selected entities records
        /// </summary>
        /// <param name="expressions">Entities to be load data from (Should be the <see cref="TEntity"/>'s navigation properties)</param>
        List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] expressions);

        #endregion

        #region Filter

        /// <summary>
        /// Filter <b><see cref="TEntity"/></b> objects by a custom expression
        /// </summary>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        List<TEntity> Filter(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Filter and order <b><see cref="TEntity"/></b> objects by custom feltering and ordering expressions
        /// </summary>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        /// <param name="orderingFunc">The <b><see cref="IOrderedQueryable"/></b> ordering expression</param>
        List<TEntity> FilterAndOrder(Expression<Func<TEntity, bool>>                       filterExpression,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc);

        #endregion
    }

}