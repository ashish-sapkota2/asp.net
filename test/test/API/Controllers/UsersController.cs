﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Test.API.Data;
using Test.API.DTOs;
using Test.API.Interface;
using Test.API.Models;

namespace Test.API.Controllers
{
    //[Authorize]
    public class UsersController : BaseApiController
    {
 
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController(IUserRepository userRepository , IMapper mapper)
        {
     
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]   
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userRepository.GetMembersAsync();

            return Ok(users);
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>>GetById(string username)
        {
            var result = await userRepository.GetMemberAsync(username);

            if(result!= null)
            {
                return result;
            }
            else
            {
                return NotFound($"No user found at Id : {username}");
            }
        }

    }
}
