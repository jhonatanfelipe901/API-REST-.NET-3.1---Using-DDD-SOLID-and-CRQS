using MyAPI.Domain.Entities;
using MyAPI.Domain.Repository;
using MyAPI.Domain.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Domain.Service
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public void Register(User user)
        {
            _userRepository.Register(user);
        }
        public User GetByUId(string uId)
        {
            return _userRepository.GetByUId(uId);
        }

    }
}
