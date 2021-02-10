using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Factories
{
    class RepoContextFactory

    {

        TContext GetInstance<TContext>() where TContext:DbContext,new()
        {
            return new TContext();
        }

    }
}
