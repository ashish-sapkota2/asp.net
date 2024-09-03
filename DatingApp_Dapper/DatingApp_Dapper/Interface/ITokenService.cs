using DatingApp_Dapper.Models;

namespace DatingApp_Dapper.Interface
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUsers user); // Task represent asynchronous operation
    }
}
