using Microsoft.Extensions.Configuration;
using MyAPI.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Service.Core
{
    public class BaseService : IBaseService
    {
        protected readonly IConfiguration _config;

        public BaseService()
        {
            _config = new ConfigurationBuilder()
                          .AddJsonFile("appsettings.Service.Core.json", true, true)
                          .Build();
        }
    }
}
