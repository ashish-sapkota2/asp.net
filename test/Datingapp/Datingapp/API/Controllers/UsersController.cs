using AutoMapper;
using Dapper;
using Datingapp.API.Data;
using Datingapp.API.DTO;
using Datingapp.API.Extensions;
using Datingapp.API.Helpers;
using Datingapp.API.Interface;
using Datingapp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Datingapp.API.Controllers
{

    public class UsersController :BaseApiController
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
           
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery]UserParams userParams)
        {
            var user = await userRepository.GetByUsername(User.GetUsername());

            userParams.CurrentUsername = user.UserName;

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = user.Gender == "male" ? "female" : "male";
            }

            var users = await userRepository.GetMembersAsync(userParams);
            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount,
                users.TotalPages);
            return Ok(users);
        }
        [HttpGet]
        [Route("{username}", Name ="GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var result = await userRepository.GetMemberAsync(username);
            return result;

        }

        [HttpPut]
        public async Task<ActionResult>UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            //var username = User.GetUsername();
            var user = await userRepository.GetByUsername(User.GetUsername());
            mapper.Map(memberUpdateDto, user);
            userRepository.Update(user);

            if (await userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to Update User");
        }

        [HttpPut("add-photo")]
        public async Task<ActionResult<PhotoDto>>AddPhoto(IFormFile file)
        {
            var user = await userRepository.GetByUsername(User.GetUsername());
            
            var result = await photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };
            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }
            user.Photos.Add(photo);

            if(await userRepository.SaveAllAsync())
            {

                //return mapper.Map<PhotoDto>(photo);
                return CreatedAtRoute("GetUser", new {username = user.UserName}, mapper.Map<PhotoDto>(photo));
            }
            

            return BadRequest("problem adding photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult>SetMainPhoto(int photoId)
        {
            var user = await userRepository.GetByUsername(User.GetUsername());
            var photo = user.Photos.FirstOrDefault(user => user.Id == photoId);
            if (photo.IsMain) return BadRequest("this is already your main photo ");

            var currentMain = user.Photos.FirstOrDefault(x=>x.IsMain);
            if(currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;
            if (await userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("failed to set main photo");
        }
        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult>DeletePhoto(int photoId)
        {
            var user = await userRepository.GetByUsername(User.GetUsername());
            var photo = user.Photos.FirstOrDefault(x=>x.Id == photoId);
            if (photo == null) return NotFound();
            if (photo.IsMain) return BadRequest("Cannot delete main photo");
            if(photo.PublicId != null)
            {
                var result =await photoService.DeletePhotoAsync(photo.PublicId);
                if(result.Error !=null) return BadRequest(result.Error);
            }
            user.Photos.Remove(photo);
            if (await userRepository.SaveAllAsync()) return Ok();
            return BadRequest("Failed to delete photo");
        }

    }
}
