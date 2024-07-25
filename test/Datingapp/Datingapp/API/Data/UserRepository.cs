using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using Datingapp.API.DTO;
using Datingapp.API.Interface;
using Datingapp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Security.Cryptography;
using System.Text;

namespace Datingapp.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext dapperDbContext;
        private readonly Data dataContext;
        private readonly IMapper mapper;

        public UserRepository(DapperDbContext dapperDbContext, Data dataContext, IMapper mapper)
        {
            this.dapperDbContext = dapperDbContext;
            this.dataContext = dataContext;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<MemberDto>> GetAll()
        {
            //var sql = @"
            //select u.*, p.url
            //from users u
            //left join photos p on u.id = p.appuserid";
            //using (var connection = dapperDbContext.CreateConnection())
            //{
            //    var task = await connection.QueryAsync<MemberDto>(sql);
            //    return task;
            //}

            return await dataContext.Users.ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<MemberDto>>GetByUsername(string username)
        {
            var sql = "select users.*,photos.url from users left join photos on users.id=photos.appuserid where username =@username";
            using (var connection = dapperDbContext.CreateConnection())
            {
                var task = await connection.QueryAsync<MemberDto>(sql, new {username= username});
                return task;
            }
        }

    }
}
