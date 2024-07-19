using Dapper;
using Datingapp.API.DTO;
using Datingapp.API.Interface;
using Datingapp.API.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Security.Cryptography;
using System.Text;

namespace Datingapp.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext dapperDbContext;

        public UserRepository(DapperDbContext dapperDbContext)
        {
            this.dapperDbContext = dapperDbContext;
        }
        public async Task<List<AppUser>> GetAll()
        {
            var sql = "select * from users";
            using (var connection = dapperDbContext.CreateConnection())
            {
                var task = await connection.QueryAsync<AppUser>(sql);
                return task.ToList();
            }
        }

        public async Task<List<AppUser>>GetByUsername(string username)
        {
            var sql = "select * from users where username =@username";
            using (var connection = dapperDbContext.CreateConnection())
            {
                var task = await connection.QueryAsync<AppUser>(sql, new {username= username});
                return task.ToList();
            }
        }
    }
}
