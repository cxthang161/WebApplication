using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Entities;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _dbContext;
        public EmployeesController(IEmployeesRepository dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _dbContext.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _dbContext.GetEmployeeById(id);

            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {

            var result = await _dbContext.AddEmployee(employee);

            return result > 0 ? Ok(employee) : BadRequest("Create employee fail!");
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployee updateEmployee)
        {
            var employee = await _dbContext.UpdateEmployee(id, updateEmployee);

            if (employee == 0)
            {
                return NotFound("Not found employee!");
            }

            return Ok("Update successed!");
        }

        [HttpDelete]
        [Route("{id:guid}")]
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
