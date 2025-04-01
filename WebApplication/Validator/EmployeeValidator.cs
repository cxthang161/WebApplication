using FluentValidation;
using WebApplication.Models.Entities;

namespace WebApplication.Validator
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        private readonly List<string> validJobs = new() { "Developer", "QC", "BA", "Manager", "QA", "Tester" };

        private bool BeAtLeast18YearsOld(DateTime? dob)
        {
            if (!dob.HasValue) return false;

            var age = DateTime.Now.Year - dob.Value.Year;
            if (dob.Value.Date > DateTime.Now.AddYears(-age)) age--;

            return age >= 18;
        }

        public EmployeeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống!")
                .MaximumLength(100).WithMessage("Tên tối đa 100 ký tự!");

            RuleFor(x => x.Job)
                .Must(job => string.IsNullOrEmpty(job) || validJobs.Contains(job))
                .WithMessage("Công việc phải là Developer, QC, BA, hoặc Manager!");

            RuleFor(x => x.DOB)
                .LessThan(DateTime.Now).WithMessage("Ngày sinh không hợp lệ!")
                .Must(BeAtLeast18YearsOld).WithMessage("Độ tuổi lao động là 18 tuổi!");
        }
    }
}
