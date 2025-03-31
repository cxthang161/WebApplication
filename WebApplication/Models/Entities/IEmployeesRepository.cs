namespace WebApplication.Models.Entities
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(Guid id);
        Task<int> AddEmployee(Employee employee);
        Task<int> UpdateEmployee(Guid id, UpdateEmployee updateEmployee);
        Task<int> DeleteEmployee(Guid id);
    }
}
