using DapperTest.Models;

namespace DapperTest.Interface
{
    public interface IDapperService
    {
        Task<List<ToDo>> GetAll();
        //Task<ToDo> GetById(int id);
        Task<string> CreateTask(ToDo todo);
        Task<string> DeleteTask(int id);
        Task<string> UpdateTask(ToDo todo);
    }
}
