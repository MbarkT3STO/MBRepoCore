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
        /// Configure and/or select the properties to be skipped
        /// </summary>
        Expression<Func<T , object>> GetSkipped();
    }
}