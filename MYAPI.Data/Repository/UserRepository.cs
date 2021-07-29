using Dapper;
using MyAPI.Domain.Entities;
using MyAPI.Domain.Repository;
using MYAPI.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MYAPI.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MyApiDBContext myApiContext) : base(myApiContext)
        {
        }

        public IEnumerable<User> GetAll()
        {
            return myApiContext.User;
        }

        public void Register(User user)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                var userParameters = new { user.UId, user.Name, user.Email, 
                                           user.Password, user.Role};

                connection.Open();

                var query = @"INSERT INTO [master].[dbo].[User] ([UId], [Name], [Email], [Password], [Role], [CreateDate])
                            VALUES (@UId ,@Name, @Email, @Password, @Role, GETDATE())";

                connection.Query<User>(query, userParameters).ToList();
            }
        }

        public User GetByUId(string uId)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                var query = @"SELECT * FROM [master].[dbo].[User] WHERE [UId] = @UId";

                return connection.Query<User>(query, new { UId = uId }).FirstOrDefault();
            }
        }
    }
}
