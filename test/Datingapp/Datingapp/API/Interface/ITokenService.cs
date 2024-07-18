using Datingapp.API.Models;

namespace Datingapp.API.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
