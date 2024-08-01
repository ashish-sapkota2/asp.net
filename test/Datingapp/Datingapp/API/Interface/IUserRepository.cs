using Datingapp.API.DTO;
using Datingapp.API.Helpers;
using Datingapp.API.Models;

namespace Datingapp.API.Interface
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetAll();
        Task<AppUser> GetByUsername(string username);
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<MemberDto> GetMemberAsync(string username);
    }
}
