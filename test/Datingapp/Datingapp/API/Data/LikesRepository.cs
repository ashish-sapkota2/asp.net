using Datingapp.API.DTO;
using Datingapp.API.Interface;
using Datingapp.API.Models;

namespace Datingapp.API.Data
{
    public class LikesRepository : ILikesRepository
       
    {
        private readonly DataContext context;

        public LikesRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await context.Likes.FindAsync(sourceUserId, likedUserId);
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
