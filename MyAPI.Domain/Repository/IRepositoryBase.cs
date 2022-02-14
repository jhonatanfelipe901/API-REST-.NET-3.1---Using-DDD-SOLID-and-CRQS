using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyAPI.Domain.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity GetById(long id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(List<TEntity> entity);
        void AddRange(IEnumerable<TEntity> entity);
        void Update(TEntity entity);
        void AddOrUpdate(TEntity entity);
        void Remove(TEntity entity);
        void Dispose();
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
    }
}
