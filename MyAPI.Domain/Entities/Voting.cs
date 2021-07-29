using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Domain.Entities
{
    public class Voting : BaseEntity
    {
        public string Subject { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool Active { get; set; }
        public string OptionOne { get; set; }
        public string OptionTwo { get; set; }

        public Voting(string subject, long userId, string description, string imagePath, bool active, string optionOne, string optionTwo)
        {
            Subject = subject;
            UserId = userId;
            Description = description;
            ImagePath = imagePath;
            Active = active;
            OptionOne = optionOne;
            OptionTwo = optionTwo;
            CreateDate = DateTime.UtcNow;
        }

        public virtual User User { get; set; }
    }
}
