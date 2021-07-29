using Microsoft.Extensions.DependencyInjection;
using MyAPI.Application.Application;
using MyAPI.Application.Contracts;
using MyAPI.Domain.Repository;
using MyAPI.Domain.Service;
using MyAPI.Domain.Service.Contracts;
using MyAPI.Service.Token;
using MYAPI.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.CrossCutting.BootStrapper
{
    public class NaviteInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Application
            services.AddTransient<IUserApplication, UserApplication>();
            services.AddTransient<IVotingApplication, VotingApplication>();
            #endregion Application

            #region Domain
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVotingService, VotingService>();
            #endregion Domain

            #region Infra
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVotingRepository, VotingRepository>();
            #endregion Infra

            #region Services
            services.AddTransient<ITokenService, TokenService>();
            #endregion Services
        }
    }
}
