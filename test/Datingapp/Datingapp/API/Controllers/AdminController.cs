using Datingapp.API.Interface;
using Datingapp.API.Models;
using Datingapp.API.Services;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IPhotoService photoService;

        public AdminController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork,
            IPhotoService photoService)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.photoService = photoService;
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
        public async Task<ActionResult> GetPhotoForModerationAsync()
        {
            var photos = await unitOfWork.PhotoRepository.GetUnapprovedPhotos();

            return Ok(photos);
        }

        [Authorize(Policy ="ModeratePhotoRole")]
        [HttpPost("approve-photo/{photoId}")]

        public async Task<ActionResult>ApprovePhoto(int photoId)
        {
            var photo = await unitOfWork.PhotoRepository.GetPhotoById(photoId);

            if (photo == null) return NotFound("could not find photo");

            photo.IsApproved = true;

            var user = await unitOfWork.UserRepository.GetUserByPhotoId(photoId);

            if(!user.Photos.Any(x=>x.IsMain)) photo.IsMain = true;

            await unitOfWork.Complete();
            return Ok();
        }

        [Authorize(Policy ="ModeratePhotoRole")]
        [HttpPost("reject-photo/{photoId}")]
        public async Task<ActionResult> RejectPhoto(int photoId)
        {
            var photo = await unitOfWork.PhotoRepository.GetPhotoById(photoId);
            if (photo == null) return NotFound("could not find photo");

            if (photo.PublicId != null)
            {
                var result = await photoService.DeletePhotoAsync(photo.PublicId);

                if(result.Result == "ok")
                {
                    unitOfWork.PhotoRepository.RemovePhoto(photo);
                }
            }
            else
            {
                unitOfWork.PhotoRepository.RemovePhoto(photo);
            }
            await unitOfWork.Complete();
            return Ok();
        }
    }
}
