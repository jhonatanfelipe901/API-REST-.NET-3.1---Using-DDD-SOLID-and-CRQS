using MyAPI.Application.Contracts;
using MyAPI.Application.DTO;
using MyAPI.Application.DTO.Request.Users;
using MyAPI.Application.DTO.Response.Users;
using MyAPI.CrossCutting.Helpers;
using MyAPI.Domain.Entities;
using MyAPI.Domain.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyAPI.CrossCutting.Helpers.Encryption;

namespace MyAPI.Application.Application
{
    public class UserApplication : BaseApplication, IUserApplication
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserApplication
        (
            IUserService userService,
            ITokenService tokenService
        )
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<BaseResponse<IEnumerable<UserListResponse>>> GetAll()
        {
            return await Task.Run(() =>
            {
                var users = _userService.GetAll();

                if (users == null)
                    return GetBaseResponseWithError<IEnumerable<UserListResponse>>("Users not found.");

                return GetBaseResponse<IEnumerable<UserListResponse>>(UserListResponse.CreateList(users));
            });
        }

        public async Task<BaseResponse<UserListResponse>> GetByEmail(string email)
        {
            return await Task.Run(() =>
            {
                var user = _userService.Get(x => x.Email == email);

                if (user == null)
                    return GetBaseResponseWithError<UserListResponse>("User not found.");

                return GetBaseResponse<UserListResponse>(UserListResponse.Create(user));
            });
        }

        public async Task<BaseResponse<UserRegisterResponse>> Register(UserRegisterRequest request)
        {
            return await Task.Run(() =>
            {
                var user = new User();

                user.CreateAddRegister(request.Name, request.Email, new Encryption(HashProvider.MD5).GetHash(request.Password), request.Role, Guid.NewGuid().ToString());

                var errors = user.ValidateRegisterNew(_userService);

                if (errors.Any())
                    return GetBaseResponseWithErrors<UserRegisterResponse>(errors);

                var userExist = _userService.Get(x => x.Email == request.Email);

                if(userExist != null)
                    return GetBaseResponseWithError<UserRegisterResponse>("Email já cadastrado.");

                _userService.Register(user);

                return GetBaseResponse<UserRegisterResponse>(UserRegisterResponse.Create(user));
            });
        }

        public async Task<BaseResponse<UserUIdResponse>> GetByUId(string uId)
        {
            return await Task.Run(() =>
            {
                var user = _userService.GetByUId(uId);

                return GetBaseResponse<UserUIdResponse>(UserUIdResponse.Create(user));
            });
        }

        public async Task<string> GenerateToken(User user)
        {
            return await Task.Run(() =>
            {
                var token = _tokenService.GenerateToken(user);

                return token;
            });
        }
    }
}
