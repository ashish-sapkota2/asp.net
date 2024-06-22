using DatingApp.Data;
using DatingApp.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext context;

        public UsersController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await  context.Users.ToListAsync();
            return users;

        }
        [HttpGet("{id}")]
        public async  Task<ActionResult<AppUser>>GetUser(int id)
        {
            var user = await  context.Users.FindAsync(id);
            if (user == null) {
                return NotFound("NO user on the given id");
            }
            return user;
        }
    }
}
