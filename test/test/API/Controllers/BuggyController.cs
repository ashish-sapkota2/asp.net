using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.API.Data;
using Test.API.Models;

namespace Test.API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext context;

        public BuggyController(DataContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = context.Users.Find(-1);
            if (thing == null) return NotFound();
            return Ok(thing);

        }

        [HttpGet("server-error")]
        public ActionResult<AppUser> GetServerError()
        {
                var thing = context.Users.Find(-1);
                if(thing==null) return StatusCode(500, "Internal Server error");
            else
            {

                return Ok(thing);
            }

        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }

    }
}
