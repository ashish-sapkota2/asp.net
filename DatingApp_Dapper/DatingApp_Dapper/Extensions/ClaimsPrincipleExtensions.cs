using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace DatingApp_Dapper.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
