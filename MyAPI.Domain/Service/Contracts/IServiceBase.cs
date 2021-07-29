using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyAPI.Domain.Service.Contracts
{
    public interface IServiceBase<TEntity>
    {
        TEntity GetById(long id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(List<TEntity> entity);
        void Update(TEntity entity);
        void AddOrUpdate(TEntity entity);
        void Remove(TEntity entity);
        bool Exists(Expression<Func<TEntity, bool>> predicate);
    }
}
