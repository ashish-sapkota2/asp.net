//using Datingapp.API.DTO;
//using Datingapp.API.Interface;
//using Datingapp.API.Models;
//using Microsoft.EntityFrameworkCore;

//namespace Datingapp.API.Data
//{
//    public class PhotoRepository : IPhotoRepository
//    {
//        private readonly DataContext context;

//        public PhotoRepository(DataContext context)
//        {
//            this.context = context;
//        }
//        public async Task<Photo> GetPhotoById(int id)
//        {
//            return await context.photos
//                .IgnoreQueryFilters()
//                .SingleOrDefaultAsync(x => x.Id == id);
//        }

//        public async Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos()
//        {
//            return await context.photos
//                .IgnoreQueryFilters()
//                .Where(p => p.IsApproved == false)
//                .Select(u => new PhotoForApprovalDto
//                {
//                    Id = u.Id,
//                    Username= u.AppUser.UserName,
//                    Url =u.Url,
//                    IsApproved=u.IsApproved
//                }).ToListAsync();
//        }

//        public void RemovePhoto(Photo photo)
//        {
//            context.photos.Remove(photo);
//        }
//    }
//}
