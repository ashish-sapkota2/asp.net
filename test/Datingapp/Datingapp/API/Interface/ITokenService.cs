using Datingapp.API.Models;

namespace Datingapp.API.Interface
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
