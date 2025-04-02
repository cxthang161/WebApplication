using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Common;
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
        public async Task<IActionResult> GetAllEmployees([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                return BadRequest(new BaseResponse<string>(null, "PageNumber and PageSize must be greater than 0", false));
            }

            var (employees, totalPages) = await _dbContext.GetAllEmployees(pageSize, pageIndex);

            var pageData = new PaginationResponse<Employee>(employees, pageIndex, totalPages);

            return Ok(new BaseResponse<PaginationResponse<Employee>>(pageData, "Success !", true));
        }

        [HttpGet]
        [Route("get-emloyee-by-id/{id:guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _dbContext.GetEmployeeById(id);

            if (employee == null) return NotFound(new BaseResponse<string>(null, "Not found !", false));

            return Ok(new BaseResponse<Employee>(employee, "success", true));
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

            return result > 0 ? Ok(new BaseResponse<Employee>(employee, "success", true)) : BadRequest(new BaseResponse<string>(null, "Create employee failed!", false));
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
                return NotFound(new BaseResponse<string>(null, "Not found employee!", false));
            }

            return Ok(new BaseResponse<string>(null, "Update successed!", true));
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("delete/{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _dbContext.DeleteEmployee(id);

            if (employee == 0)
            {
                return NotFound(new BaseResponse<string>(null, "Not found employee!", false));
            }

            return Ok(new BaseResponse<string>(null, "Delete successed!", true));
        }
    }
}
