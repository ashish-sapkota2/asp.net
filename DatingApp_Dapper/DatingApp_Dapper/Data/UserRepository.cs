using Dapper;
using DatingApp_Dapper.DTOs;
using DatingApp_Dapper.Interface;
using DatingApp_Dapper.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp_Dapper.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperConnection dapper;
        private readonly DataContext context;

        public UserRepository(DapperConnection dapper, DataContext context)
        {
            this.dapper = dapper;
            this.context = context;
        }
        public async Task<IEnumerable<MemberDto>> GetAll()
        {
            var sql = "select * from users";
            using (var connection = dapper.CreateConnection())
            {
                var task = await connection.QueryAsync<MemberDto>(sql);
                return task;
            }
        }

        public async Task<ActionResult<MemberDto>> GetByUsername(string username)
        {
            var sql = "select * from users where username =@username";
            using (var connection = dapper.CreateConnection())
            {
                var task =  await connection.QueryFirstOrDefaultAsync<MemberDto>(sql, new {username});
                return task;
            }
        }
    }
}
