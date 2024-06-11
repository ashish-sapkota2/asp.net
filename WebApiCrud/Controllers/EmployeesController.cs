using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCrud.Data;
using WebApiCrud.Models;
using WebApiCrud.Models.Entities;

namespace WebApiCrud.Controllers
{
    //localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(dbContext.Employees.ToList());

        }
        [HttpGet("{id:guid}")]
        public IActionResult GetEmployeesById(Guid id)
        {
            var employee= dbContext.Employees.Find(id);
            if (employee == null) {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]

        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;
            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id) { 
            var employee =dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok();
        }
        
    }
}
