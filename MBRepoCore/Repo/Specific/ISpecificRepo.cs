using MBRepoCore.Repo.Abstractions;

namespace MBRepoCore.Repo.Specific
{
    /// <summary>
    ///Classic repository with <see cref="IBasicRepository"/> and <see cref="IAdvancedRepository"/> features, Can be the base class for your <b><see cref="TEntity"/></b> repository
    /// </summary>
    /// <typeparam name="TEntity">The entity to create repository for</typeparam>
    public interface ISpecificRepo<TEntity>:IBasicRepository<TEntity>,IBasicRepositoryAsync<TEntity>,IAdvancedRepositoryAsync<TEntity>,IAdvancedRepository<TEntity> where TEntity : class
    {

    }
}