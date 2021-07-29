using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Application.DTO
{
    public class BaseResponse<T>
    {

        public bool Success
        {
            get
            {
                return Errors == null && ValidationsErrors == null;
            }
        }

        public T Data { get; set; }

        public IReadOnlyCollection<ErrorResponse> Errors { get; set; }
        public IReadOnlyCollection<ErrorResponse> ValidationsErrors { get; set; }
    }
}
