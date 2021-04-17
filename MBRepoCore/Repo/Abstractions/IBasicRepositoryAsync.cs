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
        /// Asynchronously, <inheritdoc cref="IBasicRepository.Get{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        ///<inheritdoc cref="IBasicRepository.Get{TEntity}"/>
        Task<List<TEntity>> GetAsync<TEntity>() where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository.GetById{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        ///<inheritdoc cref="IBasicRepository.GetById{TEntity}"/>
        Task<TEntity> GetByIdAsync<TEntity>(object pkValue) where TEntity : class;

        #endregion

        #region Add

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository.Add{TEntity}(TEntity)"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IBasicRepository.Add{TEntity}(TEntity)"/>
        Task AddAsync<TEntity>( TEntity record ) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository.Add{TEntity}(List{TEntity})"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IBasicRepository.Add{TEntity}(List{TEntity})"/>
        Task AddAsync<TEntity>( List<TEntity> records ) where TEntity : class;

        #endregion

        #region Update

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository.Update{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IBasicRepository.Update{TEntity}"/>
        Task UpdateAsync<TEntity>( TEntity record ) where TEntity : class;

        #endregion

        #region Contains

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository.Contains{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IBasicRepository.Contains{TEntity}"/>
        Task<bool> ContainsAsync<TEntity>(TEntity obj) where TEntity : class;

        #endregion

        #region Remove

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository.Remove{TEntity}(TEntity)"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IBasicRepository.Remove{TEntity}(TEntity)"/>
        Task RemoveAsync<TEntity>( TEntity record ) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository.Remove{TEntity}(List{TEntity})"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IBasicRepository.Remove{TEntity}(List{TEntity})"/>
        Task RemoveAsync<TEntity>( List<TEntity> records ) where TEntity : class;

        #endregion

    }

    /// <summary>
    /// Represent the Asynchronous base specific repository
    /// </summary>
    /// <typeparam name="TEntity">Entity to create repository for</typeparam>
    public interface IBasicRepositoryAsync<TEntity> where TEntity : class
    {

        #region Get

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository{TEntity}.Get"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<List<TEntity>> GetAsync();

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository{TEntity}.GetById"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IBasicRepository{TEntity}.GetById"/>
        Task<TEntity> GetByIdAsync( object pkValue );

        #endregion

        #region Add

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository{TEntity}.Add(TEntity)"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IBasicRepository{TEntity}.Add(TEntity)"/>
        Task AddAsync( TEntity record );

        /// <summary>
        /// Asynchronously,  <inheritdoc cref="IBasicRepository{TEntity}.Add(List{TEntity})"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IBasicRepository{TEntity}.Add(List{TEntity})"/>
        Task AddAsync( List<TEntity> records );

        #endregion

        #region Update

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository{TEntity}.Update"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IBasicRepository{TEntity}.Update"/>
        Task UpdateAsync( TEntity record );

        #endregion

        #region Contains

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository{TEntity}.Contains"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IBasicRepository{TEntity}.Contains"/>
        Task<bool> ContainsAsync( TEntity obj );

        #endregion

        #region Remove

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository{TEntity}.Remove(TEntity)"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IBasicRepository{TEntity}.Remove(TEntity)"/>
        Task RemoveAsync( TEntity record );

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IBasicRepository{TEntity}.Remove(List{TEntity})"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IBasicRepository{TEntity}.Remove(List{TEntity})"/>
        Task RemoveAsync( List<TEntity> records );

        #endregion

    }
}