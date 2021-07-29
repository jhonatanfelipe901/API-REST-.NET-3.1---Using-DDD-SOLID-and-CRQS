using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyAPI.Application.DTO.Request.Users
{
    public class UserLogInRequest
    {
        [JsonProperty("email")]
        [Required(ErrorMessage = "O email é obrigatório.")]
        public string Email { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; }
    }
}
