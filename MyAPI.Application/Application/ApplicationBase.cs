using MyAPI.Application.Contracts;
using MyAPI.Domain.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Application.Application
{
    public class ApplicationBase<TEntity> : BaseApplication, IApplicationBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> serviceBase;

        public ApplicationBase(IServiceBase<TEntity> serviceBase)
        {
            this.serviceBase = serviceBase;
        }

        public void Add(TEntity entity)
        {
            this.serviceBase.Add(entity);
        }

        public void AddOrUpdate(TEntity entity)
        {
            this.serviceBase.AddOrUpdate(entity);
        }

        public TEntity GetById(long id)
        {
            return this.serviceBase.GetById(id);
        }

        public void Remove(TEntity entity)
        {
            this.serviceBase.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            this.serviceBase.Update(entity);
        }

        public void UpdateDate(TEntity entity)
        {
            this.serviceBase.Update(entity);
        }
    }
}