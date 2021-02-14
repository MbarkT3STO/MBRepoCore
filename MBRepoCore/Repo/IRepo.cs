using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo
{
    interface IRepo<Tcontext> where  Tcontext : DbContext
    {


        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;

        TEntity GetOne<TEntity>(object pkValue) where TEntity : class;

        void Insert<TEntity>(TEntity record) where TEntity : class;

        void InsertRange<TEntity>(List<TEntity> records) where TEntity : class;

        bool Contains<TEntity>(TEntity obj) where TEntity : class;

        bool Contains<TEntity, TEntityComparer>(TEntity obj)
            where TEntity : class
            where TEntityComparer : IEqualityComparer<TEntity>, new();

        void Delete<TEntity>(TEntity           record) where TEntity : class;
        void DeleteMany<TEntity>(List<TEntity> record) where TEntity : class;

        IEnumerable<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>>         filterExpression) where TEntity : class;

        IEnumerable<TEntity> FilterWithOrder<TEntity>(Expression<Func<TEntity, bool>> filterExpression,
                                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc) where TEntity : class;
        void                 Save();


    }
}
