using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Domain.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        IEnumerable<User> GetAll();

        void Register(User user);

        User GetByUId(string uId);
    }
}
