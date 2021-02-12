using System.Linq;
using System.Threading.Tasks;
using MBRepoCore.Repo;
using Microsoft.EntityFrameworkCore;
using MBRepoCore.Exceptions;

namespace MBRepoCore.UOW
{
    /// <summary>
    /// Unit of work class to accept or decline the happened changes in the dbcontext repository
    /// </summary>
    /// <typeparam name="TContext">The same repository dbcontext</typeparam>
    class Uow<TContext> : IUow<TContext> where TContext : DbContext, new()

    {

        private readonly TContext _context;

        public Uow(Repo<TContext> repo)
        {

            //Check if TContext type and RepoContext are differents
            if (typeof(TContext) != repo.Context.GetType())
            {
                throw new MBRepoCoreExceptions.NotMatchDBContext(typeof(TContext), repo.Context.GetType());
            }

            _context = (TContext)repo.Context;
        }




        /// <summary>
        /// Accept and commit all database happened changes
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Asynchrounously, accept and commit all database happened changes
        /// </summary>
        /// <returns></returns>
        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        /// <summary>
        /// Decline all happened changes and rollback to the last saved point from database
        /// </summary>
        public void RollBack()
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
        
        /// <summary>
        /// Asynchronously, decline all happened changes and rollback to the last saved point from database
        /// </summary>
        public Task RollBackAsync()
        {
            return Task.Factory.StartNew(()=>RollBack());
        }




    }
}