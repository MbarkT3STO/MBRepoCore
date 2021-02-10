using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.UOW
{
    interface IUow<TContext> where TContext : DbContext, IDbContextFactory<TContext>, new()

    {

        void Commit();
        void RollBack();

    }
}