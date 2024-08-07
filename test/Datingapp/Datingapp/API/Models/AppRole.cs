using Microsoft.AspNetCore.Identity;

namespace Datingapp.API.Models
{
    public class AppRole:IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
