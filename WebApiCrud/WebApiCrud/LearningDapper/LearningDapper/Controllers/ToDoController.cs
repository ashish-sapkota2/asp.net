using LearningDapper.Interface;
using LearningDapper.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LearningDapper.Controllers
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

        // GET: api/<ToDoController>
        [HttpGet]
        public Task<List<ToDo>> Get()
        {
           var task =dapperService.GetAll();
            return task;
        }

        // GET api/<ToDoController>/5
        [HttpGet("{id}")]
        public async Task<ToDo> Get(int id)
        {
            var task = await dapperService.GetById(id);
            return task;
        }

        // POST api/<ToDoController>
        [HttpPost]
        public async Task<string>  Post([FromBody] ToDo toDO)
        {
            var task = await dapperService.CreateTask(toDO);
            return task;
        }

        // PUT api/<ToDoController>/5
        [HttpPut("{id}")]
        public async Task<string> Put(int id, [FromBody] ToDo toDo)
        {

            var task = await dapperService.UpdateTask(toDo);
            return task;
        }

        // DELETE api/<ToDoController>/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            var response = await dapperService.DeleteTask(id);
            return response;
        }
    }
}
