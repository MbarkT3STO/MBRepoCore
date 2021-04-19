using MBRepoCore.Exceptions;
using MBRepoCore.Repo.Generic;
using MBRepoCore.Repo.Specific;

using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;

namespace MBRepoCore.UOW
{
    /// <summary>
    /// Abstract <b>Unit Of Work</b> class to accept or decline the happened changes in both <b><see cref="SpecificRepo{TEntity}"/></b> or <b><see cref="GenericRepo{TContext}"/></b> repositories
    /// </summary>
    public abstract class BaseUow<TContext> : IUow<TContext>,IUowAsync<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        #region Constructors

        /// <summary>
        /// This constructor can be used with <see cref="GenericRepo{TContext}"/>
        /// </summary>
        /// <param name="repo">The <see cref="GenericRepo{TContext}"/> object that use the same <see cref="TContext"/></param>
        protected BaseUow(GenericRepo<TContext> repo)
        {
            //Check if TContext type and RepoContext are differents
            if (typeof(TContext) != repo.Context.GetType())
            {
                throw new MBRepoCoreExceptions.NotMatchDBContext(typeof(TContext), repo.Context.GetType());
            }

            _context = (TContext) repo.Context;
        }

        /// <summary>
        /// This constructor can be used with <see cref="SpecificRepo{TEntity}"/>
        /// </summary>
        /// <param name="dbContext">The <see cref="TContext"/> object used by <see cref="SpecificRepo{TEntity}"/></param>
        protected BaseUow(TContext dbContext)
        {
            _context = dbContext;
        }

        #endregion

        #region Commit

        /// <inheritdoc />
        public virtual void Commit()
        {
            _context.SaveChanges();
        }

        /// <inheritdoc />
        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        #endregion

        #region RollBack

        /// <inheritdoc />
        public virtual void RollBack()
        {
            foreach (var entry in _context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        /// <inheritdoc />
        public Task RollBackAsync()
        {
            return Task.Factory.StartNew(() => RollBack());
        }

        #endregion
    }
}