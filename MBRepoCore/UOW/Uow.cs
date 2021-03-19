using System.Linq;
using System.Threading.Tasks;
using MBRepoCore.Repo;
using Microsoft.EntityFrameworkCore;
using MBRepoCore.Exceptions;
using MBRepoCore.Repo.Generic;

namespace MBRepoCore.UOW
{
    /// <inheritdoc />
    public class Uow<TContext> : BaseUow<TContext> where TContext : DbContext
    {

        /// <inheritdoc />
        public Uow(GenericRepo<TContext> repo):base(repo) { }

        /// <inheritdoc />
        public Uow(TContext dbContext):base(dbContext) { }

    }
}