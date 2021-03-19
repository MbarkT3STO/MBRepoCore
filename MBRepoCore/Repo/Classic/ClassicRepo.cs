using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo.Classic
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
