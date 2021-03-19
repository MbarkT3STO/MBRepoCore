using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo
{
    /// <inheritdoc />
    public class ClassicRepo<TEntity>:BaseClassicRepo<TEntity> where TEntity:class
    {

        #region Constructors

        public ClassicRepo(DbContext context, bool lazyLoaded) : base(context,lazyLoaded)
        {

        }

        #endregion

    }
}
