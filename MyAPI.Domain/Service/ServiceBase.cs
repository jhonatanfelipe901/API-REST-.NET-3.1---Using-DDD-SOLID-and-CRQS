using Microsoft.Extensions.Configuration;
using MyAPI.Domain.Repository;
using MyAPI.Domain.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyAPI.Domain.Service
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        public IConfiguration Configuration { get; private set; }

        private readonly IRepositoryBase<TEntity> _repositoryBase;

        public ServiceBase(IRepositoryBase<TEntity> repositoryBase)
        {
            Configuration = new ConfigurationBuilder()
                .Build();

            _repositoryBase = repositoryBase;
        }

        public virtual TEntity GetById(long id)
        {
            return _repositoryBase.GetById(id);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _repositoryBase.Get(predicate);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return _repositoryBase.GetList(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            _repositoryBase.Add(entity);
        }

        public virtual void AddRange(List<TEntity> entity)
        {
            _repositoryBase.AddRange(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _repositoryBase.Update(entity);
        }

        public virtual void AddOrUpdate(TEntity entity)
        {
            _repositoryBase.AddOrUpdate(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            _repositoryBase.Remove(entity);
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _repositoryBase.Exists(predicate);
        }

        public void Dispose()
        {
            _repositoryBase.Dispose();
        }
    }
}

