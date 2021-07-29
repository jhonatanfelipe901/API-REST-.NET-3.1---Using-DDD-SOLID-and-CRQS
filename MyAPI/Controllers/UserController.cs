using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Application.Contracts;
using MyAPI.Application.DTO.Request.Users;
using MyAPI.Application.DTO.Response.Users;

namespace MyAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid) return ResponseBadRequest<UserRegisterRequest>(GetModelStateErrors());

            if (request.Password != request.ConfirmPassword) return ResponseBadRequest<UserRegisterResponse>(GetModelStateErrors());

            var response = await _userApplication.Register(request);

            return BaseResponse(response);
        }

        [HttpGet]
        [Route("loggedin")]
        public async Task<IActionResult> GetUserLoggedIn()
        {
           var identity = HttpContext.User.Identity as ClaimsIdentity;

           var userId = identity.Claims.Where(x => x.Type == "user_id").FirstOrDefault()?.Value;

           var user = await _userApplication.GetByUId(userId);

           return BaseResponse<UserUIdResponse>(user);
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userApplication.GetAll();

            return BaseResponse<IEnumerable<UserListResponse>>(users);
        }


    }
}
