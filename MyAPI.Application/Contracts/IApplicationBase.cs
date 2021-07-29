using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Application.Contracts
{
    public interface IApplicationBase<TEntity>
    {

        TEntity GetById(long id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void AddOrUpdate(TEntity entity);
        void Remove(TEntity entity);
        void UpdateDate(TEntity entity);
        
    }
}
