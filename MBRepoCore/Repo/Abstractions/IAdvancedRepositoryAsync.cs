using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MBRepoCore.Repo.Abstractions
{
    /// <summary>
    /// Represent the Asynchronous advanced Generic repository
    /// </summary>
    public interface IAdvancedRepositoryAsync
    {
        #region Get

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.GetAll{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"><inheritdoc cref="IAdvancedRepository.GetAll{TEntity}"/></typeparam>
        /// <param name="relatedEntitiesToBeLoaded"><inheritdoc cref="IAdvancedRepository.GetAll{TEntity}"/></param>
        Task<List<TEntity>> GetAsyc<TEntity>(params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.GetMany{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"><inheritdoc cref="IAdvancedRepository.GetMany{TEntity}"/></typeparam>
        /// <param name="filterExpression"><inheritdoc cref="IAdvancedRepository.GetMany{TEntity}"/></param>
        Task<List<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filterExpression"></param>
        /// <param name="relatedEntitiesToBeLoaded"> </param>
        Task<List<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class;

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"> </typeparam>
        /// <param name="pkValue"></param>
        /// <param name="relatedEntitiesToBeLoaded"></param>
        Task<TEntity> GetFirstAsync<TEntity>(object pkValue, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class;

        #endregion

        #region Update

        /// <summary>
        /// Asynchronously
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filterExpression"></param>
        /// <param name="updateAction"></param>
        Task UpdateMany<TEntity>(Expression<Func<TEntity, bool>> filterExpression, Action<TEntity> updateAction) where TEntity : class;

        #endregion

        #region Exist

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pkValue"></param>
        /// <returns><see cref="Task"/></returns>
        Task<bool> IsExist<TEntity>(object pkValue) where TEntity : class;

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="selectExpression"></param>
        /// <returns><see cref="Task"/></returns>
        Task<bool> IsExist<TEntity>(Expression<Func<TEntity, bool>> selectExpression) where TEntity : class;

        #endregion
    }

    /// <summary>
    /// Represent the Asynchronous advanced speific Generic repository
    /// </summary>
    public interface IAdvancedRepositoryAsync<TEntity>
    {

        #region Get

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <param name="relatedEntitiesToBeLoaded"></param>
        Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded);

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filterExpression"></param>
        Task<List<TEntity>> GetMany(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="relatedEntitiesToBeLoaded"></param>
        Task<List<TEntity>> GetMany(Expression<Func<TEntity, bool>> filterExpression, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded);

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <param name="pkValue"></param>
        /// <param name="relatedEntitiesToBeLoaded"></param>
        Task<TEntity> GetOne(object pkValue, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded);

        #endregion

        #region Update

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="updateAction"></param>
        Task UpdateMany(Expression<Func<TEntity, bool>> filterExpression, Action<TEntity> updateAction);

        #endregion

        #region Exist

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <param name="pkValue"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> IsExist(object pkValue);

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <param name="selectExpression"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> IsExist(Expression<Func<TEntity, bool>> selectExpression);

        #endregion

    }
}