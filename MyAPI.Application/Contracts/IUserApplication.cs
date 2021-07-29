using MyAPI.Application.DTO;
using MyAPI.Application.DTO.Request.Users;
using MyAPI.Application.DTO.Response.Users;
using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Contracts
{
    public interface IUserApplication
    {
        Task<BaseResponse<IEnumerable<UserListResponse>>> GetAll();
        Task<BaseResponse<UserListResponse>> GetByEmail(string email);
        Task<BaseResponse<UserUIdResponse>> GetByUId(string uId);
        Task<BaseResponse<UserRegisterResponse>> Register(UserRegisterRequest request);
        Task<string> GenerateToken(User user);
    }
}
