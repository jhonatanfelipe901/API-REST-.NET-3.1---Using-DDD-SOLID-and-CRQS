using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Application.Contracts;
using MyAPI.Application.DTO.Request.Users;
using MyAPI.Application.DTO.Response.Users;
using MyAPI.CrossCutting.Helpers;
using MyAPI.Domain.Entities;
using MyAPI.Service.Token;
using static MyAPI.CrossCutting.Helpers.Encryption;

namespace MyAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/login")]
    public class LoginController : Controller
    {
        private readonly IUserApplication _userApplication;

        public LoginController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpPost]
        public async Task<dynamic> Authenticate([FromBody] UserLogInRequest request)
        {
            if (!ModelState.IsValid) return false;

            bool validCredentials = false;

            var userBase = _userApplication.GetByEmail(request.Email).Result.Data;

            if (userBase == null) return false;

            validCredentials = (request.Email == userBase.Email &&
                                new Encryption(HashProvider.MD5).GetHash(request.Password) == userBase.Password);

            if (!validCredentials) return false;

            var user = new User()
            {
                UId = userBase.UId,
                Name = userBase.Name,
                Email = userBase.Email,
                Role = userBase.Role
            };

            var token = _userApplication.GenerateToken(user).Result;

            return UserLoginResponse.Create(true, "Success", token);
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";

    }
}
