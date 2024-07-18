using Datingapp.API.DTO;
using Datingapp.API.Models;

namespace Datingapp.API.Interface
{
    public interface IUserRepository
    {
        Task<List<AppUser>> GetAll();
        Task<List<AppUser>> GetByUsername(string username);
        Task<string> UpdateData(RegisterDto user);
    }
}
