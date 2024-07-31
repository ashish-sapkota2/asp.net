using CloudinaryDotNet.Actions;

namespace Datingapp.API.Interface
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult>DeletePhotoAsync(string publicId);

    }
}
