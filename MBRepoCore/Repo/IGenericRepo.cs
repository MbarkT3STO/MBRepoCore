using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo
{
    interface IGenericRepo<TContext> where TContext : DbContext
    {

        // Routins

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <returns></returns>
        List<TEntity> GetAll<TEntity>() where TEntity : class;

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records, and load related selected entities records
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <returns></returns>
        List<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] expressions) where TEntity : class;

        /// <summary>
        /// Get One <b><see cref="TEntity"/></b> record, based on the primary key value
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="pkValue">The primary key value</param>
        /// <returns></returns>
        TEntity GetOne<TEntity>(object pkValue) where TEntity : class;

        /// <summary>
        /// Add one <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <typeparam name="TEntity">Entity to add into</typeparam>
        /// <param name="record">The record to be added</param>
        void AddOne<TEntity>(TEntity record) where TEntity : class;

        /// <summary>
        /// Add a range of <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <typeparam name="TEntity">Entity to insert into</typeparam>
        /// <param name="records">Records to be inserted</param>
        void AddMany<TEntity>(List<TEntity> records) where TEntity : class;

        /// <summary>
        /// Check if <b><see cref="TEntity"/></b> contains an object
        /// </summary>
        /// <typeparam name="TEntity">Entity to be look in</typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns></returns>
        bool Contains<TEntity>(TEntity obj) where TEntity : class;

        /// <summary>
        ///  Check if <b><see cref="TEntity"/></b> contains an object based on a custom <b><see cref="IEqualityComparer{T}"/></b>
        /// </summary>
        /// <typeparam name="TEntity">Entity to be look in</typeparam>
        /// <typeparam name="TEntityComparer">The custom TEntity <b><see cref="IEqualityComparer{T}"/></b></typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns></returns>
        bool Contains<TEntity, TEntityComparer>(TEntity obj)
            where TEntity : class
            where TEntityComparer : IEqualityComparer<TEntity>, new();

        /// <summary>
        /// Remove One <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <typeparam name="TEntity">Entity to remove from</typeparam>
        /// <param name="record">The record to be removed</param>
        void RemoveOne<TEntity>(TEntity record) where TEntity : class;

        /// <summary>
        /// Remove a range of <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <typeparam name="TEntity">Entity to delete from</typeparam>
        /// <param name="records">Records to be deleted</param>
        void RemoveMany<TEntity>(List<TEntity> records) where TEntity : class;

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
        /// <param name="orderingFunc">The <b><see cref="IOrderedQueryable{T}"/></b> ordering expression</param>
        /// <returns></returns>
        List<TEntity> FilterAndOrder<TEntity>(Expression<Func<TEntity, bool>>                       filterExpression,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc)
            where TEntity : class;
    }
}