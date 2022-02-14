using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Application.DTO.Response.Users
{
    public class UserLoginResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        public static UserLoginResponse Create(bool success, string message, string token)
        {

            return new UserLoginResponse()
            {
                Success = success,
                Message = message,
                Token = token
            };
        }
    }
}
