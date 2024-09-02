using DatingApp_Dapper.Data;
using DatingApp_Dapper.DTOs;
using DatingApp_Dapper.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp_Dapper.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DapperConnection dapper;

        public AccountController(DapperConnection dapper) 
        {
            this.dapper = dapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();

            var users = new AppUsers();
            users.UserName = registerDto.Username.ToLower();

            users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));


    }
}
