using WebApplication.Models.Entities;

namespace WebApplication.Repositories.Employees
{
    public interface IEmployeesRepository
    {
        Task<(IEnumerable<Employee>, int)> GetAllEmployees(int pageSize, int pageIndex);
        Task<Employee?> GetEmployeeById(Guid id);
        Task<int> AddEmployee(Employee employee);
        Task<int> UpdateEmployee(Employee updateEmployee);
        Task<int> DeleteEmployee(Guid id);
    }
}
