using System;
using System.Linq.Expressions;

namespace MBRepoCore.Interfaces
{
    /// <summary>
    /// Represents a <b><see cref="T"/></b> properties to be skipped
    /// </summary>
    /// <typeparam name="T">A type to represent its properties</typeparam>
    public interface ISkippable<T> where T : class
    {
        /// <summary>
        /// Configure the properties to be skiped
        /// </summary>
        Expression<Func<T , object>>[] GetSkiped();
    }
}