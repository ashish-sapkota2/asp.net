using Datingapp.API.DTO;
using Datingapp.API.Interface;
using Datingapp.API.Models;

namespace Datingapp.API.Data
{
    public class LikesRepository : ILikesRepository
       
    {
        public LikesRepository(DataContext context) { }
        public Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetUserWithLikes(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
