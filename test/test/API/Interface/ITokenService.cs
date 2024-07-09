using Test.API.Models;

namespace Test.API.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
