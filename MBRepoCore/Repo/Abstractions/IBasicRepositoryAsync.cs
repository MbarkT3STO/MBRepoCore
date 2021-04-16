using System.Collections.Generic;
using System.Threading.Tasks;


namespace MBRepoCore.Repo.Abstractions
{
    /// <summary>
    /// Represent the Asynchronous base Generic repository
    /// </summary>
    public interface IBasicRepositoryAsync
    {

        #region Get

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Task<List<TEntity>> GetAsync<TEntity>() where TEntity : class;

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pkValue"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync<TEntity>(object pkValue) where TEntity : class;

        #endregion

        #region Add

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="record"></param>
        Task AddAsync<TEntity>(TEntity record) where TEntity : class;

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="records"></param>
        Task AddAsync<TEntity>(List<TEntity> records) where TEntity : class;

        #endregion

        #region Update

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="record"></param>
        Task UpdateAsync<TEntity>( TEntity record ) where TEntity : class;

        #endregion

        #region Contains

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<bool> ContainsAsync<TEntity>(TEntity obj) where TEntity : class;

        #endregion

        #region Remove

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="record"></param>
        Task RemoveAsync<TEntity>( TEntity record ) where TEntity : class;

        /// <summary>
        /// Asynchronously,
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="records"></param>
        Task RemoveAsync<TEntity>( List<TEntity> records ) where TEntity : class;

        #endregion

    }
}