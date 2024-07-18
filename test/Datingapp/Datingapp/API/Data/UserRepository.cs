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

        public async Task<string> UpdateData(RegisterDto user)
        {
            
            string response = string.Empty;
            var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            var passwordSalt = hmac.Key;
            var sql = "update users set username=@username, passwordHash= @passwordHash, PasswordSalt= @passwordSalt where username=@username ";
            var parameters = new DynamicParameters();
            parameters.Add("username", user.Username);
            parameters.Add("PasswordHash", passwordHash);
            parameters.Add("PasswordSalt", passwordSalt);
            try
            {

            using(var connection = dapperDbContext.CreateConnection())
            {
                var task = await connection.ExecuteAsync(sql, parameters);

                    response = task > 0 ? "Update Successful" : "Update Failed";


            }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error updating user: {ex.Message}");
                response = "Update Failed";
            }
                return response;
        }
    }
}
