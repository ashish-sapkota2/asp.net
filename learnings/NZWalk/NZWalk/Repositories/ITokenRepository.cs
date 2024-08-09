using Microsoft.AspNetCore.Identity;

namespace NZWalk.Repositories
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser user, List<String> roles);
    }
}
