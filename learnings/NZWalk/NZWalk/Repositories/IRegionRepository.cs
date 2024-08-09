using NZWalk.Models;

namespace NZWalk.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>>GetAllAsync();
    }
}
