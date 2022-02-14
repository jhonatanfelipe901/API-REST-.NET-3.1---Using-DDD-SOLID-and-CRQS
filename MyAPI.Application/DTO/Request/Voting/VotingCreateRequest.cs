using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyAPI.Application.DTO.Request.Voting
{
    public class VotingCreateRequest
    {
        [JsonProperty("subject")]
        [Required(ErrorMessage = "O subject é obrigatório.")]
        public string Subject { get; set; }

        [JsonProperty("userId")]
        [Required(ErrorMessage = "O subject é obrigatório.")]
        public long UserId { get; set; }

        [JsonProperty("description")]
        [Required(ErrorMessage = "A description é obrigatória.")]
        public string Description { get; set; }

        [JsonProperty("image")]
        [Required(ErrorMessage = "A imagem é obrigatória.")]
        public string Image { get; set; }

        [JsonProperty("imageType")]
        [Required(ErrorMessage = "A imagem é obrigatória.")]
        public string ImageType { get; set; }

        [JsonProperty("firstOption")]
        [Required(ErrorMessage = "A imagem é obrigatória.")]
        public string FirstOption { get; set; }

        [JsonProperty("secondOption")]
        [Required(ErrorMessage = "A imagem é obrigatória.")]
        public string SecondOption { get; set; }


    }
}
