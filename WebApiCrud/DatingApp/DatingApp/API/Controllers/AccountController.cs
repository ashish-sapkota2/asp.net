using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Entity;
using DatingApp.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext context;
        private readonly ITokenService tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            //using means when we are done with using the particular class it will be disposed correctly

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)), // changing string to byte
                Passwordsalt = hmac.Key
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user =await context.Users
                .SingleOrDefaultAsync(x => x.UserName ==loginDto.Username);
            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.Passwordsalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++) {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Passwrod");

            }
            return new UserDto
            {
                Username=user.UserName,
                Token=tokenService.CreateToken(user)
            };
        }
        
        private async Task<bool>UserExists(string username)
        {
            return await context.Users.AnyAsync(x=> x.UserName == username.ToLower());
        }
    }
}
