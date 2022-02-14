using MyAPI.Domain.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Domain.Validations.User
{
    public class RegisterNewUserValidation : UserValidation
    {
        public RegisterNewUserValidation(IUserService userService) : base(userService)
        {
            ValidateEmail();
        }
    }
}
