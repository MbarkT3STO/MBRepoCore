using MBRepoCore.Repo.Generic;
using Microsoft.EntityFrameworkCore;

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