using System.Threading.Tasks;
using MBRepoCore.Repo;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.UOW
{
    class Uow<TContext> : IUow<TContext> where TContext : DbContext, IDbContextFactory<TContext>, new()

    {

        private TContext _context { get; }

        public Uow(Repo<TContext> repo)
        {
            _context = (TContext) repo.Context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Task CommitAsync()
        {
            return Task.Factory.StartNew(() => _context.SaveChangesAsync());
        }

        public void RollBack()
        {

        }
    }
}