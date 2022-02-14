using MyAPI.Domain.Service.Contracts;
using MyAPI.Domain.Validations.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAPI.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

        public void CreateAddRegister(string name, string email, string password, string role, string uId)
        {
            UId = uId;
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }

        public virtual ICollection<Voting> Votings { get; set; }

        public IEnumerable<string> ValidateRegisterNew(IUserService userService)
        {
            return new RegisterNewUserValidation(userService).Validate(this).Errors.Select(e => e.ErrorMessage);
        }
    }
}
