using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiseSet.Data;
using PractiseSet.Models;
using PractiseSet.Models.DTO;
using PractiseSet.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace PractiseSet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext context;
        private readonly IMapper mapper;
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(EmployeeDbContext context, IMapper mapper, IEmployeeRepository employeeRepository) {
            this.context = context;
            this.mapper = mapper;
            this.employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<ActionResult<Employee>> GetAll()
        {
            //var employee = await context.Employees.ToListAsync();
            //if (employee == null)
            //{
            //    return NotFound("No any data found");
            //}
            //return Ok(employee);

            // get data from database 
            var employeedomain = await employeeRepository.GetAllAsync();
            
            //map domain model to DTO
            var employeeDto = mapper.Map<List<Employee>>(employeedomain);
            return Ok(employeeDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var employee = await context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound($"No details on Employee Id: {id}");
            }
            return Ok(employee);   
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> PutData(PostEmployeeDto employee)
        {
            //   var record = context.Employees.Add(employee);
            //    await context.SaveChangesAsync();
            //    if (record == null) {
            //        return BadRequest();
            //    }
            //    return Ok(await context.Employees.ToListAsync());
            //}

            //convert to domain model
            var employeedomain = mapper.Map<Employee>(employee);
            await context.Employees.AddAsync(employeedomain);
            await context.SaveChangesAsync();

            return Ok(mapper.Map<EmployeeDto>(employeedomain));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Update(int id, UpdateEmployeeDto updateEmployeeDto) {
            //fetch data 

            var record = await context.Employees.FindAsync(id);
            
            if (record == null)
            {
                return NotFound($"No details found at Id: {id}");
            }
            record.PhoneNumber = updateEmployeeDto.PhoneNumber;
            record.Email  = updateEmployeeDto.Email;
            record.Department= updateEmployeeDto.Department;
            record.Name = updateEmployeeDto.Name;
            var update = await context.SaveChangesAsync();
            if (update == 1)
            {
                Console.WriteLine("success");
            }
            else
            {
                Console.WriteLine("someerror");
            }
            var employeedto = mapper.Map<Employee>(record);
            return Ok(employeedto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            var record = await context.Employees.FindAsync(id);
            if (record == null)
            {
                return BadRequest("No record found");
            }
            context.Employees.Remove(record);
            await context.SaveChangesAsync();
            return Ok(record);
        }
    }

}
