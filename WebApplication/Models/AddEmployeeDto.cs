namespace WebApplication.Models
{
    public class AddEmployeeDto
    {
        public required string Name { get; set; }
        public string? Job { get; set; }
        public DateTime? DOB { get; set; }
    }
}
