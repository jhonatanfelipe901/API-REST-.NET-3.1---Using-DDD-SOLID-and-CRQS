using MyAPI.Domain.Entities;
using MYAPI.Data.Context;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;
using MyAPI.Domain.Repository;

namespace MYAPI.Data.Repository
{
    public class VotingRepository : RepositoryBase<Voting>, IVotingRepository
    {
        public VotingRepository(MyApiDBContext myApiContext) : base(myApiContext)
        {
        }

        public IEnumerable<Voting> GetAll(int userId)
        {
            return myApiContext.Voting.Where(x => x.UserId == userId);
        }

        public void Create(Voting voting)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                var query = @"INSERT INTO [master].[dbo].[Voting] ([UserId], [Subject], [Description], 
                                                         [ImagePath], [Active], [OptionOne], [OptionTwo], [CreateDate]) 
                                                         VALUES (@UserId, @Subject, @Description, @ImagePath, 1, @OptionOne, @OptionTwo, GETDATE())";

                connection.Query<Voting>(query, new { voting.UserId, voting.Subject, voting.Description, 
                                                      voting.ImagePath, voting.Active, voting.OptionOne, voting.OptionTwo
                });;
            }
        }
    }
}
