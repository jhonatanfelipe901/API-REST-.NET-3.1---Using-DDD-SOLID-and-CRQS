using MyAPI.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Application.DTO.Response.Votings
{
    public class VotingDetailsResponse
    {

        [JsonProperty("success")]
        public string Subject { get; set; }

        [JsonProperty("success")]
        public string Description { get; set; }

        [JsonProperty("success")]
        public string ImagePath { get; set; }

        [JsonProperty("success")]
        public bool Active { get; set; }

        [JsonProperty("optionOne")]
        public string OptionOne { get; set; }

        [JsonProperty("optionTwo")]
        public string OptionTwo { get; set; }

        public static VotingDetailsResponse Create(Voting voting)
        {
            if (voting == null)
                return null;

            return new VotingDetailsResponse()
            {
                Subject = voting.Subject,
                Description = voting.Description,
                ImagePath = voting.ImagePath,
                Active = voting.Active,
                OptionOne = voting.OptionOne,
                OptionTwo = voting.Subject
            };
        }
    }
}
