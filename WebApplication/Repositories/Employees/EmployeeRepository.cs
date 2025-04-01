using Dapper;
using Microsoft.Data.SqlClient;
using WebApplication.Models.Entities;

namespace WebApplication.Repositories.Employees
{
    public class EmployeeRepository : IEmployeesRepository
    {
        private readonly string? _dbContext;

        public EmployeeRepository(IConfiguration configuration)
        {
            _dbContext = configuration.GetConnectionString("DefaultCollection");
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            using var connection = new SqlConnection(_dbContext);
            string sql = "SELECT * FROM Employees";
            return await connection.QueryAsync<Employee>(sql);
        }

        public async Task<Employee?> GetEmployeeById(Guid id)
        {
            using var connection = new SqlConnection(_dbContext);
            string sql = "SELECT * FROM Employees WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Id = id });
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            using var connection = new SqlConnection(_dbContext);
            string sql = "INSERT INTO Employees (Id, Name, Job, DOB) VALUES (@Id, @Name, @Job, @DOB)";
            employee.Id = Guid.NewGuid();
            return await connection.ExecuteAsync(sql, employee);
        }

        public async Task<int> UpdateEmployee(Employee updateEmployee)
        {
            using var connection = new SqlConnection(_dbContext);

            string getEmployeeSql = "SELECT DOB FROM Employees WHERE Id = @Id";
            var oldDob = await connection.QueryFirstOrDefaultAsync<DateTime?>(getEmployeeSql, new { updateEmployee.Id });

            string sql = "UPDATE Employees SET Name = @Name, Job = @Job, DOB = @DOB WHERE Id = @Id";
            var parameters = new
            {
                updateEmployee.Id,
                updateEmployee.Name,
                updateEmployee.Job,
                DOB = updateEmployee.DOB ?? oldDob

            };

            return await connection.ExecuteAsync(sql, parameters);
        }

        public async Task<int> DeleteEmployee(Guid id)
        {
            using var connection = new SqlConnection(_dbContext);
            string sql = "DELETE FROM Employees WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
