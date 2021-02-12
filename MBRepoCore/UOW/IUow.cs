using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.UOW
{
    interface IUow<TContext> where TContext : DbContext, new()

    {

        void Commit();
        void RollBack();

    }
}