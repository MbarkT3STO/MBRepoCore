using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.UOW
{ 
    public interface IUow<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Accept and commit all database happened changes
        /// </summary>
        void Commit();

        /// <summary>
        /// Decline all happened changes and rollback to the last saved point from database
        /// </summary>
        void RollBack();

    }
}