﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Domain.Entities.Paged
{
    public class PagedResult<T> : PagedResultBase
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
