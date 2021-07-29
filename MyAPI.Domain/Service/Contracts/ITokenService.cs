using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Domain.Service.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
