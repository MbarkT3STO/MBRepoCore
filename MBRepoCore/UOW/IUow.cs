using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace MBRepoCore
{
    interface IUow<TContext> where TContext : DbContext, IDbContextFactory<TContext>, new()

    {

        void Commit();
        void RollBack();

    }
}