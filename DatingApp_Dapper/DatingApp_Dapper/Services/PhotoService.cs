using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp_Dapper.Helpers;
using DatingApp_Dapper.Interface;
using Microsoft.Extensions.Options;

namespace DatingApp_Dapper.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        public PhotoService(IOptions<CloudinarySettings>config) 
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            cloudinary = new Cloudinary(acc);
        }
        public Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            throw new NotImplementedException();
        }
    }
}
