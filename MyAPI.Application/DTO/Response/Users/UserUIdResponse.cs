using MyAPI.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Application.DTO.Response.Users
{
    public class UserUIdResponse
    {
        [JsonProperty("UId")]
        public string UId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public static UserUIdResponse Create(User user)
        {
            return new UserUIdResponse()
            {
                UId = user.UId,
                Name = user.Name
            };
        }
    }
}
