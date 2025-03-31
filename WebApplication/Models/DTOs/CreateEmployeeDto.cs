using FluentValidation;
using WebApplication.Models.Entities;

namespace WebApplication.Models.DTOs
{
    public class CreateEmployeeDto : AbstractValidator<Employee>
    {
        private readonly List<string> validJobs = new() { "Developer", "QC", "BA", "Manager" };

        public CreateEmployeeDto()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống!")
                .MaximumLength(100).WithMessage("Tên tối đa 100 ký tự!");

            RuleFor(x => x.Job)
                .Must(job => string.IsNullOrEmpty(job) || validJobs.Contains(job))
                .WithMessage("Công việc phải là Developer, QC, BA, hoặc Manager!");

            RuleFor(x => x.DOB)
                .LessThan(DateTime.Now).WithMessage("Ngày sinh không hợp lệ!");
        }
    }
}
