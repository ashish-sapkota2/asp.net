using PractiseSet.Models;

namespace PractiseSet.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
    }
}
