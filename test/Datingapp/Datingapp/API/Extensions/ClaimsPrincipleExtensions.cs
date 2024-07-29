using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Datingapp.API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        } 
    }
}
