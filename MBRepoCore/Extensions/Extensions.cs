using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MBRepoCore.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Get all members access for a <see cref="List{T}"/> of <see cref="Expression{TDelegate}"/>
        /// </summary>
        /// <typeparam name="T">Type that the <b><see cref="Expression"/>s</b></typeparam> based on
        /// <param name="source">The <see cref="List{T}"/> of <see cref="Expression{TDelegate}"/> to get <see cref="MemberInfo"/> from</param>
        /// <returns><see cref="IEnumerable{MemberInfo}"/></returns>
        public static IEnumerable<MemberInfo> GetMemberAccess<T>( this List<Expression<Func<T , object>>> source )
        {
            return source.SelectMany( expression => expression.GetMemberAccessList() );
        }
    }
}
