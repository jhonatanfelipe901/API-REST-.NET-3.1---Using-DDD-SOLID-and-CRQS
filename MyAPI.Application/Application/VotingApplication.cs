using MyAPI.Application.Contracts;
using MyAPI.Application.DTO;
using MyAPI.Application.DTO.Request.Voting;
using MyAPI.Application.DTO.Response.Users;
using MyAPI.Application.DTO.Response.Votings;
using MyAPI.CrossCutting.Helpers;
using MyAPI.Domain.Entities;
using MyAPI.Domain.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Application
{
    public class VotingApplication : BaseApplication, IVotingApplication
    {
        private readonly IVotingService _votingService;
        private readonly IUserService _userService;

        public VotingApplication
        (
            IVotingService votingService,
            IUserService userService
        )
        {
            _votingService = votingService;
            _userService = userService;
        }

        //public async Task<BaseResponse<IEnumerable<UserListResponse>>> GetAll(VotingCreateRequest request)
        //{
            
        //}

        public async Task<BaseResponse<VotingCreateResponse>> Create(VotingCreateRequest request, string userUId)
        {
            return await Task.Run(() =>
            {

                var user = _userService.Get(x => x.UId == userUId);

                if (user == null)
                    return GetBaseResponseWithError<VotingCreateResponse>("Usuário não encontrado.");

                //deleting head from image64
                var file64 = request.Image.Substring(request.Image.LastIndexOf(',') + 1);

                //deleting head from image64type
                var fileType = request.ImageType.Substring(request.ImageType.LastIndexOf('/') + 1);

                var voting = new Voting(request.Subject, user.Id, request.Description, "", true, request.FirstOption, request.SecondOption);

                var errors = _votingService.Create(voting, file64, fileType);

                if (errors.Any())
                    return GetBaseResponseWithErrors<VotingCreateResponse>(errors);

                return GetBaseResponse<VotingCreateResponse>(VotingCreateResponse.Create(voting, true));
            });
        }

        public async Task<BaseResponse<VotingDetailsResponse>> GetById(long id, string userUId)
        {
            return await Task.Run(() =>
            {
                var user = _userService.Get(x => x.UId == userUId);

                var voting = _votingService.Get(x => x.Id == id && x.Active == true);

                if (voting == null)
                    return GetBaseResponseWithError<VotingDetailsResponse>("Votação não existe.");

                if (voting.UserId != user.Id)
                    return GetBaseResponseWithError<VotingDetailsResponse>("Não autorizado.");

                return GetBaseResponse<VotingDetailsResponse>(VotingDetailsResponse.Create(voting));
            });
        }
    }
}
