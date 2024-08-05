using Datingapp.API.DTO;
using Datingapp.API.Helpers;
using Datingapp.API.Models;

namespace Datingapp.API.Interface
{
    public interface ILikesRepository
    {
        Task<UserLike>GetUserLike(int sourceUserId, int likedUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}
