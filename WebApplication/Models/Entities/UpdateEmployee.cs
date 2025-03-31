namespace WebApplication.Models.Entities
{
    public class UpdateEmployee
    {
        public required string Name { get; set; }
        public string? Job { get; set; }
        public DateTime? DOB { get; set; }
    }
}
