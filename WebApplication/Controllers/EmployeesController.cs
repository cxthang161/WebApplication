using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Models.Entities;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public EmployeesController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_dbContext.Employees.ToList());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeesById(Guid id) { 
            var employee = _dbContext.Employees.Find(id);

            if(employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployees(AddEmployeeDto addEmployeeDto)
        {

            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Job = addEmployeeDto.Job,
                DOB = addEmployeeDto.DOB,
            };

            _dbContext.Employees.Add(employeeEntity);
            _dbContext.SaveChanges();

            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployee updateEmployee)
        {
            var employee = _dbContext.Employees.Find(id);
            
            if (employee is null)
            {
                return NotFound();
            };

            employee.Name = updateEmployee.Name;
            employee.Job = updateEmployee.Job;
            if (updateEmployee.DOB.HasValue)
            {
                employee.DOB = updateEmployee.DOB;
            }

            _dbContext.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id) 
        {
            var employee = _dbContext.Employees.Find(id);

            if(employee is null)
            {
                return NotFound();
            };

            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();

            return Ok(employee);
        }
    }
}
