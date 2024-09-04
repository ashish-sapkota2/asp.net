using DatingApp_Dapper.DTOs;
using DatingApp_Dapper.Interface;
using DatingApp_Dapper.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp_Dapper.Controllers
{
    public class UsersController: BaseApiController
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet]
        public async Task<ActionResult<MemberDto>> GetAllUsers()
        {
            return Ok( await userRepository.GetAll());
        }
        [HttpGet("ByUsername")]
        public async Task<ActionResult<MemberDto>> GetUserByUsername(string username)
        {
            var response=await userRepository.GetByUsername(username);
            return Ok(response);
        }
    }
}
