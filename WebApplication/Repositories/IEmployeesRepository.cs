using WebApplication.Models.Entities;

namespace WebApplication.Repositories
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(Guid id);
        Task<int> AddEmployee(Employee employee);
        Task<int> UpdateEmployee(Employee updateEmployee);
        Task<int> DeleteEmployee(Guid id);
    }
}
