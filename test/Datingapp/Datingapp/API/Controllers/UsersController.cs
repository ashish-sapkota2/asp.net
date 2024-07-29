using AutoMapper;
using Dapper;
using Datingapp.API.Data;
using Datingapp.API.DTO;
using Datingapp.API.Extensions;
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
        public async Task<IActionResult> GetAllUsers()
        {
            var users= await userRepository.GetMembersAsync();
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

    }
}
