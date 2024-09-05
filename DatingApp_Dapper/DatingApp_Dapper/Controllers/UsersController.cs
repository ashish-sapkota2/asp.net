using Dapper;
using DatingApp_Dapper.Data;
using DatingApp_Dapper.DTOs;
using DatingApp_Dapper.Extensions;
using DatingApp_Dapper.Interface;
using DatingApp_Dapper.Models;
using DatingApp_Dapper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp_Dapper.Controllers
{
    public class UsersController: BaseApiController
    {
        private readonly IUserRepository userRepository;
        private readonly DapperConnection dapper;
        private readonly DataContext context;
        private readonly IPhotoService photoService;

        public UsersController(IUserRepository userRepository, DapperConnection dapper,
            DataContext context,IPhotoService photoService)
        {
            this.userRepository = userRepository;
            this.dapper = dapper;
            this.context = context;
            this.photoService = photoService;
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
        [Authorize]
        [HttpPut]
        public async Task<ActionResult>UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.GetUsername();
            Console.WriteLine($"Username from claims: {username}");
            using (var connection = dapper.CreateConnection())
            {
                var sql = "UPDATE users SET Introduction=@Introduction, LookingFor=@LookingFor,Interest=@Interest," +
                    "City=@City,Country=@Country where username=@username";
                var result = await connection.ExecuteAsync(sql, new
                {
                    Introduction = memberUpdateDto.Introduction,
                    LooKingFor = memberUpdateDto.LookingFor,
                    Interest = memberUpdateDto.Interest,
                    City = memberUpdateDto.City,
                    Country = memberUpdateDto.Country,
                    username
                });
                if (result > 0)
                {
                    return Ok("Update Successful");
                }
                else
                {
                    return Ok("No rows affected");
                }
            }
        }
        [Authorize]
        [HttpPut("add-photo")]
        public async Task<ActionResult<PhotoDto>>AddPhoto(IFormFile file)
        {
            var user = await userRepository.GetByUsername(User.GetUsername());
            Console.WriteLine(user);
            var result = await photoService.AddPhotoAsync(file);
            if(result.Error !=null) return BadRequest(result.Error.Message);
            if (user.Photos == null)
            {
                user.Photos = new List<Photo>();
            }
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };
            if (user.Photos?.Count == 0)
            {
                photo.IsMain = true;
            }
            user.Photos.Add(photo);
            context.SaveChangesAsync();
            return Ok("done");
        }
    }
}
