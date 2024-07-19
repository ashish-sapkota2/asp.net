using Dapper;
using Datingapp.API.Data;
using Datingapp.API.DTO;
using Datingapp.API.Interface;
using Datingapp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Datingapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
           
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<List<AppUser>> GetAllUsers()
        {
            return await userRepository.GetAll();
        }
        [HttpGet("Username")]
        public async Task<List<AppUser>> GetUser(string username)
        {
            return await userRepository.GetByUsername(username);
        }

    }
}
