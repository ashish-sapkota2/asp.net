using Datingapp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Datingapp.API.Controllers
{
    public class AdminController: BaseApiController
    {
        private readonly UserManager<AppUser> userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize(Policy ="RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r=>r.Role)
                .OrderBy(u=>u.UserName)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(r=>r.Role.Name).ToList()
                })
                .ToListAsync();

            return Ok(users);

        }

        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var user = await userManager.FindByNameAsync(username);
            if (user == null) return NotFound("could not find user");

            var userRoles = await userManager.GetRolesAsync(user);
            foreach(var roleToRemove in userRoles)
            {
            var result = await userManager.RemoveFromRoleAsync(user, roleToRemove);
            if (!result.Succeeded) return BadRequest("Failed to remove from roles");
            }
            foreach(var roleToAdd in selectedRoles)
            {

            var result = await userManager.AddToRoleAsync(user, roleToAdd);
            if (!result.Succeeded) return BadRequest("Failed to add roles");
            }


            return Ok(await userManager.GetRolesAsync(user));
        }
        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photos-to-moderate")]
        public ActionResult GetPhotoForModeration()
        {
            return Ok("Admin or Moderators can see this");
        }
    }
}
