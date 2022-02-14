using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyAPI.Application.DTO.Request.Users
{
    public class UserRegisterRequest
    {
        [JsonProperty("name")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        [JsonProperty("email")]
        [Required(ErrorMessage = "O email é obrigatório.")]
        public string Email { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; }

        [JsonProperty("confirmPassword")]
        [Required(ErrorMessage = "A confirmação da senha é obrigatória.")]
        public string ConfirmPassword { get; set; }

        [JsonProperty("role")]
        [Required(ErrorMessage = "A função do usuário é obrigatória.")]
        public string Role { get; set; }
    }
}
