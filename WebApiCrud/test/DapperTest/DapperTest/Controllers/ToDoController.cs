using DapperTest.Interface;
using DapperTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IDapperService dapperService;

        public ToDoController(IDapperService dapperService)
        {
            this.dapperService = dapperService;
        }

        [HttpGet]
        public Task<List<ToDo>> Get()
        {
            var task = dapperService.GetAll();
            return task;
        }
        [HttpPost]
        public Task<string> CreateTak(ToDo todo)
        {
            var task = dapperService.CreateTask(todo);
            return task;
        }
        [HttpDelete("{id}")]
        public  Task<string>Delete(int id)
        {
            var task = dapperService.DeleteTask(id);
            return task;
        }
        [HttpPut]
        public Task<string> Update(ToDo todo)
        {
            var task = dapperService.UpdateTask(todo);
            return task;
        }

    }
}
