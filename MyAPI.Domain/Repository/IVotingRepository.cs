using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Domain.Repository
{
    public interface IVotingRepository : IRepositoryBase<Voting>
    {

        void Create(Voting voting);
    }
}
