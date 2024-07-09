using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Test.API.Data;
using Test.API.Models;

namespace Test.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext context;

        public UsersController(DataContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var result = await context.Users.ToListAsync();
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("No Users found ");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult>GetById(int id)
        {
            var result = await context.Users.FindAsync(id);
            if(result!= null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound($"No user found at Id : {id}");
            }
        }

    }
}
