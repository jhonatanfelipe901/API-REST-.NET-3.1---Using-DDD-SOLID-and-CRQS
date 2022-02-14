using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Domain.Service.Contracts
{
    public interface IVotingService : IServiceBase<Voting>
    {
        IEnumerable<string> Create(Voting voting, string image64, string imageExtension);

    }
}

    

