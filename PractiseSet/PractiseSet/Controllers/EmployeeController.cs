using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiseSet.Data;
using PractiseSet.Models;
using PractiseSet.Models.DTO;
using PractiseSet.Repository;

namespace PractiseSet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly PractiseDbContext practiseDbContext;
        private readonly IEmployee employeerepositoy;
        private readonly IMapper mapper;

        public EmployeeController(PractiseDbContext practiseDbContext , IEmployee employeerepositoy, IMapper mapper) 
        {
            this.practiseDbContext = practiseDbContext;
            this.employeerepositoy = employeerepositoy;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employeedomain = await employeerepositoy.GetAll();

            var employeeDto = mapper.Map<Employee>(employeedomain);

            return Ok(employeeDto);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var employee = await practiseDbContext.Employees.FindAsync(id);
            return Ok(mapper.Map<EmployeeDto>(employee));

        }

        [HttpPost]
        public async Task<IActionResult> Insert(Employee employee)
        {
             practiseDbContext.Employees.Add(employee);
            await practiseDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id ,Employee employee)
        {
            var result = practiseDbContext.Employees.FirstOrDefault(x => x.Id == id);
            if(result == null)
            {
                return BadRequest(result);
            }
            result.Id = id;
            result.Name =employee.Name;
            result.Email =employee.Email;
            result.Addresss= employee.Addresss;
            result.phone= employee.phone;
            result.Department = employee.Department;
            practiseDbContext.SaveChanges();
            return Ok(result);
        }
        

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await practiseDbContext.Employees.FindAsync(id);
            if(result == null)
            {
                return NotFound($"Employee with {id} not found");
            }
            else
            {
                
                 practiseDbContext.Remove(result);
                await practiseDbContext.SaveChangesAsync();
            }
            return Ok(result);

        }
    }
}
