using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Test.API.DTOs;
using Test.API.Interface;
using Test.API.Models;

namespace Test.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly DapperContext dapperContext;

        public UserRepository(DataContext context, IMapper mapper, DapperContext dapperContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.dapperContext = dapperContext;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            //return await context.Users.
            //    Where(x => x.UserName == username).ProjectTo<MemberDto>(mapper.ConfigurationProvider).SingleOrDefaultAsync();
            var sql = "select * from users where username =@username";
            using (var connection = dapperDbContext.CreateConnection())
            {
                var task = await connection.QueryAsync<AppUser>(sql, new { username = username });
                return task.ToList();
            }
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            //var sql = "select * from users";
            //using (var connection = dapperContext.CreateConnection())
            //{
            //    var task = await connection.QueryAsync<MemberDto>(sql);
            //    return task;
            //}
          var result =  await context.Users.ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AppUser>> GetUserAsync()
        {
            return await context.Users.Include(u => u.Photos).ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await context.Users.Include(u=>u.Photos).SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            context.Entry(user).State= EntityState.Modified;
        }
    }
}
