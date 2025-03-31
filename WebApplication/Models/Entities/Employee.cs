namespace WebApplication.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Job { get; set; }
        public DateTime? DOB { get; set; }

    }
}
