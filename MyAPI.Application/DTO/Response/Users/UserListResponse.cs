using MyAPI.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAPI.Application.DTO.Response.Users
{
    public class UserListResponse
    {
        [JsonProperty("id")]
        public string UId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }

        public static UserListResponse Create(User user)
        {
            if (user == null)
                return null;

            return new UserListResponse()
            {
                UId = user.UId,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role,
                Password = user.Password
            };
        }

        public static IEnumerable<UserListResponse> CreateList(IEnumerable<User> users)
        {
            if (users == null) return null;

            return users.Select(h => Create(h));
        }
    }
}
