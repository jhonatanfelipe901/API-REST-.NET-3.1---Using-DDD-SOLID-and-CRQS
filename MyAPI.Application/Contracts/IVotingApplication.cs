using MyAPI.Application.DTO;
using MyAPI.Application.DTO.Request.Voting;
using MyAPI.Application.DTO.Response.Users;
using MyAPI.Application.DTO.Response.Votings;
using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Contracts
{
    public interface IVotingApplication
    {
        Task<BaseResponse<VotingCreateResponse>> Create(VotingCreateRequest request, string userUId);

        Task<BaseResponse<VotingDetailsResponse>> GetById(long id, string userUId);
    }
}
