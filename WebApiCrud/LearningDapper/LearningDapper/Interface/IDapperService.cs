using LearningDapper.Models;

namespace LearningDapper.Interface
{
    public interface IDapperService
    {
        Task<List<ToDo>> GetAll();
        Task<ToDo> GetById(int id);
        Task<string> CreateTask(ToDo toDo);
        Task<string>UpdateTask(ToDo toDo);
        Task<string> DeleteTask(int id);

    }
}
