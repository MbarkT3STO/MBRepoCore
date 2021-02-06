using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MBRepo
{
    interface IRepo<Tcontext> where  Tcontext : DbContext, new()
    {


        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;

        TEntity GetOne<TEntity>(object pkValue) where TEntity : class;

        void Insert<TEntity>(TEntity record) where TEntity : class;

        void InsertRange<TEntity>(List<TEntity> records) where TEntity : class;

        bool Contains<TEntity>(TEntity obj) where TEntity : class;

        bool Contains<TEntity, TEntityComparer>(TEntity obj)
            where TEntity : class
            where TEntityComparer : IEqualityComparer<TEntity>, new();

        void Delete<TEntity>(TEntity record) where TEntity : class;

        void Save();


    }
}
