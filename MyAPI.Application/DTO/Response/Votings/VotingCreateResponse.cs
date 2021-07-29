using MyAPI.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Application.DTO.Response.Votings
{
    public class VotingCreateResponse
    {
        [JsonProperty("voting_id")]
        public double Id { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        public static VotingCreateResponse Create(Voting voting, bool success)
        {
            if (voting == null)
                return null;

            return new VotingCreateResponse()
            {
                Id = voting.Id,
                Success = success
            };
        }
    }
}
