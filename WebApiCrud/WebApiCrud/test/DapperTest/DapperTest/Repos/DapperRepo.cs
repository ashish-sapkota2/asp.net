using Dapper;
using DapperTest.Data;
using DapperTest.Interface;
using DapperTest.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DapperTest.Repos
{
    public class DapperRepo: IDapperService
    {
        private readonly DataContext context;

        public DapperRepo(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<ToDo>> GetAll()
        {
            var sql = "select * from toDo";
            using (var connection = context.CreateConnection())
            {
                var task = await connection.QueryAsync<ToDo>(sql);
                return task.ToList();
            }
        }
        public async Task<string> CreateTask(ToDo toDo)
        {
            var sql = "insert into toDo(Name, Description, Stauts, CreatedAt) values( @name, @description,@stauts,@createdAt)";
            var parameters = new DynamicParameters();
            parameters.Add("name", toDo.Name);
            parameters.Add("description", toDo.Description);
            parameters.Add("stauts", toDo.Stauts);
            parameters.Add("createdAt", toDo.CreatedAt);
            using (var connection = context.CreateConnection())
            {
                var task = await connection.ExecuteAsync(sql, parameters);


            }
            return "created";
        }
        public async Task<string>DeleteTask(int id)
        {
            var search = "select count(1) from toDo where id =@id";
            var delete = "delete from toDo where id=@id";
            using(var connection = context.CreateConnection())
            {
                var task = await connection.QuerySingleOrDefaultAsync<bool>(search, new { id});
                if (!task)
                {
                    return $"No any task in id :  {id}";
                }
                await connection.ExecuteAsync(delete, new { id });
                return $"Task on id: {id} deleted successfully ";
            }
        }
        public async Task<string>UpdateTask(ToDo toDo)
        {
            var sql = "update toDo set  name=@name, description=@description, stauts=@stauts, createdAt =@createdAt where id=@id";
            var parameters = new DynamicParameters();
            parameters.Add("id", toDo.Id);
            parameters.Add("name", toDo.Name);
            parameters.Add("description", toDo.Description);
            parameters.Add("stauts", toDo.Stauts);
            parameters.Add("createdAt", toDo.CreatedAt);
            using (var connection = context.CreateConnection())
            {
                var task = await connection.ExecuteAsync(sql, parameters);


            }
            return "Update Successfully";
        }
    }
}
