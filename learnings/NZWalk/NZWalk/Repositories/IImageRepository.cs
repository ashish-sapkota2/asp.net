using NZWalk.Models;

namespace NZWalk.Repositories
{
    public interface IImageRepository
    {
        Task<Image>Upload(Image image);


    }
}
