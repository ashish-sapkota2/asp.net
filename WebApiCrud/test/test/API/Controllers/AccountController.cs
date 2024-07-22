using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;
using System.Text;
using Test.API.Data;
using Test.API.DTOs;
using Test.API.Interface;
using Test.API.Models;
using Test.API.Services;

namespace Test.API.Controllers
{
    public class AccountController: BaseApiController
    {
        private readonly DataContext context;
        private readonly ITokenService tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Username)){
                return BadRequest("UserName already exits! please try other");

            }
            else
            {
                using var hmac = new HMACSHA512();
                var user = new AppUser
                {
                    UserName = registerDto.Username.ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                    PasswordSalt =hmac.Key
                };
                context.Users.Add(user);
                await context.SaveChangesAsync();

                return new UserDto { Username = user.UserName, Token = tokenService.CreateToken(user) };
             
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (user == null)
            {
                return Unauthorized("Username doesnot exist");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }

            return new UserDto
            {
                Username = loginDto.Username,
                Token = tokenService.CreateToken(user)
            };

        }        
               

        private async Task<bool>UserExists(string username)
        {
            return await context.Users.AnyAsync(x=>x.UserName== username.ToLower());
        }
    }
}
