using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MBRepoCore.UOW
{
    public interface IUowAsync<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Asynchronously, <inheritdoc cref="BaseUow{TContext}.Commit"/>
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Asynchronously, <inheritdoc cref="BaseUow{TContext}.RollBack"/>
        /// </summary>
        Task RollBackAsync();
    }
}