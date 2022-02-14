using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Application.Contracts;
using MyAPI.Application.DTO.Request.Voting;
using MyAPI.Application.DTO.Response.Votings;

namespace MyAPI.Controllers
{ 
    [Produces("application/json")]
    [Route("api/voting")]
    public class VotingController : BaseController
    {
        private readonly IVotingApplication _votingApplication;

        public VotingController(IVotingApplication votingApplication)
        {
            _votingApplication = votingApplication;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VotingCreateRequest request)
        {
            if (!ModelState.IsValid) return ResponseBadRequest<VotingCreateResponse>(GetModelStateErrors());

            var response = await _votingApplication.Create(request, GetUserUId());

            return BaseResponse<VotingCreateResponse>(response);
        }

        [HttpGet]
        [Route("choose/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var voting = await _votingApplication.GetById(id, GetUserUId());

            return BaseResponse<VotingDetailsResponse>(voting);
        }
    }
}