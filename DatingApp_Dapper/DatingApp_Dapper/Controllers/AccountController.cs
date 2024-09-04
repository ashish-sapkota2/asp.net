using Dapper;
using DatingApp_Dapper.Data;
using DatingApp_Dapper.DTOs;
using DatingApp_Dapper.Interface;
using DatingApp_Dapper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp_Dapper.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DapperConnection dapper;
        private readonly DataContext context;
        private readonly ITokenService tokenService;

        public AccountController(DapperConnection dapper,DataContext context, ITokenService tokenService) 
        {
            this.dapper = dapper;
            this.context = context;
            this.tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) 
            {
                return BadRequest("Username already Exists");
            }
            using var hmac = new HMACSHA512();

            var users = new AppUsers();
            users.UserName = registerDto.Username.ToLower();
            users.KnownAs= registerDto.KnownAs.ToLower();
            users.City = registerDto.City.ToLower();
            users.Country= registerDto.Country.ToLower();
            users.Gender = registerDto.Gender.ToLower();
            users.DateOfBirth = registerDto.DateOfBirth;
            users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            users.PasswordSalt = hmac.Key;
            context.Users.Add(users);
            var result = await context.SaveChangesAsync();
            if (result < 0)
            {
                return BadRequest("Error creating user");
            }
            return new UserDto { 
            Username= users.UserName,
            Token= await tokenService.CreateToken(users)
            };
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var sql = "select * from users where username=@username";
            using (var connection = dapper.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<AppUsers>(sql, new {loginDto.Username });
                if(user == null)
                {
                    return BadRequest("Username doesnot exist");
                }
              using var hmac = new HMACSHA512(user.PasswordSalt);
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
                for(int i=0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != user.PasswordHash[i])
                    {
                        return Unauthorized("Invalid Password");
                    }
                }
                return new UserDto
                {
                    Username = user.UserName,
                    Token = await tokenService.CreateToken(user)
                };
            }
        }

        private async Task<bool> UserExists(string username)
        {
            var sql = "select username from users where username=@username ";
            using (var connection = dapper.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<string>(sql, new { username });
                return (user != null) ? true : false;
            }
        }

    }
}
