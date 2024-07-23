using Dapper;
using LearningDapper.dapperContext;
using LearningDapper.Interface;
using LearningDapper.Models;

namespace LearningDapper.Repo
{
    public class dapperRepo : IDapperService
    {
        private readonly DapperDbContext dapperDbContext;

        public dapperRepo(DapperDbContext dapperDbContext)
        {
            this.dapperDbContext = dapperDbContext;
        }

        public async Task<string> CreateTask(ToDo toDo)
        {
            string response = String.Empty;
            var sql = "Insert into todos(Id,Name,Description,Status, CreatedAt) values(@id,@name,@description,@status, @createdAt)";
            var parameters = new DynamicParameters();
            parameters.Add("id", toDo.Id);
            parameters.Add("name", toDo.Name);
            parameters.Add("description", toDo.Description);
            parameters.Add("status",toDo.Status);
            parameters.Add("createdAt", toDo.CreatedAt);
            using (var connection = dapperDbContext.CreateConnection())
            {
                var task = await connection.ExecuteAsync(sql,parameters);
                response = "pass";
            }
            return response;

        }

        public async Task<string> DeleteTask(int id)
        {
            string response = String.Empty;
            var sql = "Delete from todos where id= @id";
  
            using (var connection = dapperDbContext.CreateConnection())
            {
                var task = await connection.ExecuteAsync(sql, new { id});
                response = "pass";
            }
            return response;

        }

        public async Task<List<ToDo>> GetAll()
        {
            var sql = "select * from todos";
            using (var connection = dapperDbContext.CreateConnection()) {
                var task =await connection.QueryAsync<ToDo>(sql);
                return task.ToList();
            }
        }

        public async Task<ToDo> GetById(int id)
        {
            var sql = "select * from todos where id= @id";
            using (var connection = dapperDbContext.CreateConnection())
            {
                var task = await connection.QueryFirstOrDefaultAsync<ToDo>(sql, new { id });
                return task;
            }
        }

        public async Task<string> UpdateTask(ToDo toDo)
        {
            string response = String.Empty;
            var sql = "update todos set id=@id,name =@name,description=@description,status=@status";
            var parameters = new DynamicParameters();
            parameters.Add("id", toDo.Id);
            parameters.Add("name", toDo.Name);
            parameters.Add("description", toDo.Description);
            parameters.Add("status", toDo.Status);
            using (var connection = dapperDbContext.CreateConnection())
            {
                var task = await connection.ExecuteAsync(sql, parameters);
                response = "pass";
            }
            return response;
        }
    }
}
