using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo
{
    public interface IRepoProperties
    {
        /// <summary>
        /// A <b><see cref="TContext"/></b> object as property
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// Determine if Lazy Loading either activate or not
        /// </summary>
        bool LazyLoaded { get; set; }
    }
}