using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiseSet.Data;
using PractiseSet.Models;

namespace PractiseSet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly PractiseDbContext practiseDbContext;

        public EmployeeController(PractiseDbContext practiseDbContext) 
        {
            this.practiseDbContext = practiseDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employees = await practiseDbContext.Employees.ToListAsync();
            return Ok(employees);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var employee = await practiseDbContext.Employees.FindAsync(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Employee employee)
        {
             practiseDbContext.Employees.Add(employee);
            await practiseDbContext.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
