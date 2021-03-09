using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo
{
    interface IRepo<TContext> where  TContext : DbContext
    {
        // Properties
        DbContext Context    { get; }
        bool      LazyLoaded { get; set; }



        // Routins
        List<TEntity> GetAll<TEntity>() where TEntity : class;

        TEntity GetOne<TEntity>(object pkValue) where TEntity : class;

        void AddOne<TEntity>(TEntity record) where TEntity : class;

        void AddMany<TEntity>(List<TEntity> records) where TEntity : class;

        bool Contains<TEntity>(TEntity obj) where TEntity : class;

        bool Contains<TEntity, TEntityComparer>(TEntity obj)
            where TEntity : class
            where TEntityComparer : IEqualityComparer<TEntity>, new();

        void RemoveOne<TEntity>(TEntity           record) where TEntity : class;
        void RemoveMany<TEntity>(List<TEntity> record) where TEntity : class;

        List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>>         filterExpression) where TEntity : class;

        List<TEntity> FilterAndOrder<TEntity>(Expression<Func<TEntity, bool>> filterExpression,
                                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc) where TEntity : class;
        void                 Save();


    }
}
