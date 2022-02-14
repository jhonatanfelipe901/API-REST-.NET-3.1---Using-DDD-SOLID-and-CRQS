using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
