using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using MyAPI.Domain.Entities;
using MyAPI.Domain.Service.Contracts;

namespace MyAPI.Domain.Validations.User
{
    public class UserValidation : AbstractValidator<MyAPI.Domain.Entities.User>
    {
        private static IUserService _userService;

        public UserValidation(IUserService userService)
        {
            _userService = userService;
        }

        protected void ValidateEmail()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O Email é obrigatósrio.")
                .EmailAddress().WithMessage("Email inválido.");
        }
    }
}
