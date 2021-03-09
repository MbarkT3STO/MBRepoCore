using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.UOW
{ 
    public interface IUow<TContext> where TContext : DbContext, new()
    {
        void Commit();
        void RollBack();

    }
}