using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MBRepoCore.Repo
{
    public interface IClassicRepo<TEntity> where TEntity:class
    {
        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAll();

        /// <summary>
        /// Get One <b><see cref="TEntity"/></b> record, based on the primary key value
        /// </summary>
        /// <param name="pkValue">The primary key value</param>
        /// <returns></returns>
        TEntity GetOne(object pkValue);

        /// <summary>
        /// Add one <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <param name="record">The record to be added</param>
        void AddOne(TEntity record);

        /// <summary>
        /// Add a range of <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <param name="records">Records to be inserted</param>
        void AddMany(List<TEntity> records);

        /// <summary>
        /// Check if <b><see cref="TEntity"/></b> contains an object
        /// </summary>
        /// <param name="obj">The object to be looking for</param>
        /// <returns></returns>
        bool Contains(TEntity obj);

        /// <summary>
        ///  Check if <b><see cref="TEntity"/></b> contains an object based on a custom <b><see cref="IEqualityComparer{T}"/></b>
        /// </summary>
        /// <typeparam name="TEntityComparer">The custom <b><see cref="TEntity"/> <see cref="IEqualityComparer{T}"/></b></typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns></returns>
        bool Contains<TEntityComparer>(TEntity obj) where TEntityComparer : IEqualityComparer<TEntity>, new();

        /// <summary>
        /// Remove One <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <param name="record">The record to be removed</param>
        void RemoveOne(TEntity record);

        /// <summary>
        /// Remove a range of <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <param name="records">Records to be deleted</param>
        void RemoveMany(List<TEntity> records);

        /// <summary>
        /// Filter <b><see cref="TEntity"/></b> objects by a custom expression
        /// </summary>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        /// <returns></returns>
        List<TEntity> Filter(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Filter and order <b><see cref="TEntity"/></b> objects by custom feltering and ordering expressions
        /// </summary>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        /// <param name="orderingFunc">The <b><see cref="IOrderedQueryable{T}"/></b> ordering expression</param>
        /// <returns></returns>
        List<TEntity> FilterAndOrder(Expression<Func<TEntity, bool>>                       filterExpression,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc);


    }
}