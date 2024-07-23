using Datingapp.API.DTO;
using Datingapp.API.Models;

namespace Datingapp.API.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<MemberDto>> GetAll();
        Task<List<AppUser>> GetByUsername(string username);
    }
}
