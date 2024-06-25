using PractiseSet.Models;

namespace PractiseSet.Repository
{
    public interface IEmployee
    {
        Task<List<Employee>> GetAll();
    }
}
