using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Factories
{
    class RepoContextFactory

    {

        TContext GetInstance<TContext>(DbContextOptions options) where TContext:DbContext
        {
            TContext context = (TContext) Activator.CreateInstance(typeof(TContext), new object[] {options});

            return context;
        }

    }
}
