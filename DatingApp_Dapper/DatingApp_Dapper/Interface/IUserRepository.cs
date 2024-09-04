using DatingApp_Dapper.DTOs;
using DatingApp_Dapper.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp_Dapper.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<MemberDto>> GetAll();
        Task<ActionResult<MemberDto>> GetByUsername(string username);
    }
}
