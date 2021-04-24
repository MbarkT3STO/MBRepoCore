using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo.Specific
{
    /// <inheritdoc />
    public class SpecificRepo<TEntity> : BaseSpecificRepo<TEntity> where TEntity : class
    {
        #region Constructors

        public SpecificRepo(DbContext context, bool lazyLoaded = false) : base(context, lazyLoaded)
        {
        }

        #endregion
    }
}