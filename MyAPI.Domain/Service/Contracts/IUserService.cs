using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Domain.Service.Contracts
{
    public interface IUserService : IServiceBase<User>
    {
        IEnumerable<User> GetAll();

        void Register(User user);

        User GetByUId(string uId);
    }
}
