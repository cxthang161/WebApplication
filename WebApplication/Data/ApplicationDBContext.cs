using Microsoft.EntityFrameworkCore;
using WebApplication.Models.Entities;

namespace WebApplication.Data
{

    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
