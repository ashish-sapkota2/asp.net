using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using Datingapp.API.DTO;
using Datingapp.API.Helpers;
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
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public UserRepository(DapperDbContext dapperDbContext, DataContext dataContext, IMapper mapper)
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

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = dataContext.Users.AsQueryable();

            query = query.Where(u => u.UserName != userParams.CurrentUsername);
            query = query.Where(u => u.Gender == userParams.Gender);

            var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

            query = query.Where(u=>u.DateOfBirth>=minDob && u.DateOfBirth<=maxDob);

            query = userParams.OrderBy switch
            {
                "Created" => query.OrderByDescending(u => u.Created), //for creates
                _ => query.OrderByDescending(u => u.LastActive) //default
            };

            return await PagedList<MemberDto>.CreatedAsync(query.ProjectTo<MemberDto>(mapper
                .ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);

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

        //public async Task<AppUser> GetUserByPhotoId(int photoId)
        //{
        //    return await dataContext.Users
        //        .Include(u => u.Photos)
        //        .IgnoreQueryFilters()
        //        .Where(p => p.Photos.Any(p => p.Id == photoId))
        //        .FirstOrDefaultAsync();
        //}
    }
}
