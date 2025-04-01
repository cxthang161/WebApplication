using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Entities;
using WebApplication.Repositories.Employees;

namespace WebApplication.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _dbContext;
        private readonly IValidator<Employee> _validator;
        public EmployeesController(IEmployeesRepository dbContext, IValidator<Employee> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _dbContext.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet]
        [Route("get-emloyee-by-id/{id:guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _dbContext.GetEmployeeById(id);

            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            var validationResult = await _validator.ValidateAsync(employee);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _dbContext.AddEmployee(employee);

            return result > 0 ? Ok(employee) : BadRequest("Create employee failed!");
        }

        [Authorize]
        [HttpPut]
        [Route("update/{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployee updateEmployee)
        {
            var parameter = new Employee
            {
                Id = id,
                Name = updateEmployee.Name,
                Job = updateEmployee.Job,
                DOB = updateEmployee.DOB
            };
            var validationResult = await _validator.ValidateAsync(parameter);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var employee = await _dbContext.UpdateEmployee(parameter);

            if (employee == 0)
            {
                return NotFound("Not found employee!");
            }

            return Ok("Update successed!");
        }

        [Authorize]
        [HttpDelete]
        [Route("delete/{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _dbContext.DeleteEmployee(id);

            if (employee == 0)
            {
                return NotFound("Not found employee!");
            }
            ;

            return Ok("Delete successed!");
        }
    }
}
