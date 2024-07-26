using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using Datingapp.API.DTO;
using Datingapp.API.Interface;
using Datingapp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.VisualBasic;
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
        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await dataContext.Users.Where(x => x.UserName == username).ProjectTo<MemberDto>(
                    mapper.ConfigurationProvider).SingleOrDefaultAsync();
 }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            var result = await dataContext.Users.ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AppUser>> GetAll()
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

            return await dataContext.Users.ProjectTo<AppUser>(mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AppUser>GetByUsername(string username)
        {
            return await dataContext.Users.Include(u => u.Photos).SingleOrDefaultAsync(x => x.UserName == username);
            //var sql = "select users.*,photos.url from users left join photos on users.id=photos.appuserid where username =@username";
            //using (var connection = dapperDbContext.CreateConnection())
            //{
            //    var task =  connection.Query<AppUser>(sql, new {username= username});
            //    return task;
            //}
        }

        public async Task<bool> SaveAllAsync()
        {
            return await dataContext.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            dataContext.Entry(user).State = EntityState.Modified;
        }
    }
}
