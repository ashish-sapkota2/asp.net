using AutoMapper;
using Dapper;
using Datingapp.API.Data;
using Datingapp.API.DTO;
using Datingapp.API.Interface;
using Datingapp.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Datingapp.API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly DapperDbContext dapperDbContext;
        private readonly ITokenService tokenService;
        //private readonly DataContext context;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser>userManager, SignInManager<AppUser>signInManager,
            DapperDbContext dapperDbContext, ITokenService tokenService,
            //DataContext context, 
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dapperDbContext = dapperDbContext;
            this.tokenService = tokenService;
            //this.context = context;
            this.mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest($"{registerDto.Username} already exists");

            var user = mapper.Map<AppUser>(registerDto);
            //using var hmac = new HMACSHA512();
            user.UserName = registerDto.Username.ToLower();
            //user.PasswordHash= hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            //user.PasswordSalt = hmac.Key;
            //context.Users.Add(user);
            //await context.SaveChangesAsync();

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) return BadRequest(result.Errors); 

            return new UserDto
            {
                Username = user.UserName,
                Token = await tokenService.CreateToken(user),
                KnownAs= user.KnownAs,
                Gender= user.Gender
            };
            //if(await UserExists(registerDto.Username))
            //{
            //    return BadRequest($"Username: {registerDto.Username} already exists");
            //}
          
            //var response = string.Empty;
            //using var hmac = new HMACSHA512();
            //var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            //var passwordSalt = hmac.Key;
            //var sql = "insert into users(UserName, PasswordHash,PasswordSalt) values(@username, @passwordHash, @passwordSalt)";
            //var parameters = new DynamicParameters();
            //parameters.Add("username", registerDto.Username);
            //parameters.Add("passwordHash",passwordHash);  
            //parameters.Add("passwordsalt",passwordSalt);   
            //using(var connection = dapperDbContext.CreateConnection())
            //{
            //    var task = await connection.ExecuteAsync(sql, parameters);
            //}
            //return registerDto;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            //var sql = "select *,photos.url from users left join photos on users.id=photos.appuserid where username =@username";
            //using(var connection = dapperDbContext.CreateConnection())
            //{
            //    var user = await connection.QueryFirstOrDefaultAsync<AppUser>(sql, new { username = loginDto.Username });
                if (user==null)
                {
                    return BadRequest("Username doesnot exists"); 
                }
                //using var hmac = new HMACSHA512(user.PasswordSalt);
                //var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
                //for(int i=0; i < computeHash.Length; i++)
                //{
                //    if (computeHash[i] != user.PasswordHash[i])
                //    {
                //        return Unauthorized("Invalid Password");
                //    }
                //}

                var result = await signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password,false);

            if (!result.Succeeded) return Unauthorized();
            
                return new UserDto
                { 
                    Username = user.UserName,
                    Token = await tokenService.CreateToken(user),
                    PhotoUrl = user?.Photos?.FirstOrDefault(x=>x.IsMain)?.Url,
                    KnownAs= user?.KnownAs,
                    Gender= user?.Gender,
                  

                };
            //}
        }

        private async Task<bool>UserExists(string username)
        {
            return await userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
            //bool response;
            //var sql = "select username from aspnetusers where username=@username";
            //using (var connection = dapperDbContext.CreateConnection())
            //{
            //    var user = await connection.QueryFirstOrDefaultAsync<string>(sql, new { username });
            //    return (user!=null) ? true : false;
            //    //return user != null;
            //}

        }
            
    }
}
