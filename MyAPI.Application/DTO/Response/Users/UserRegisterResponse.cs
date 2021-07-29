using MyAPI.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAPI.Application.DTO.Response.Users
{
    public class UserRegisterResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public static UserRegisterResponse Create(User user)
        {
            if (user == null)
                return null;

            return new UserRegisterResponse()
            {
                Name = user.Name,
                Email = user.Email
            };
        }

        public static IEnumerable<UserRegisterResponse> CreateList(IEnumerable<User> users)
        {
            if (users == null) return null;

            return users.Select(h => Create(h));
        }
    }
}
