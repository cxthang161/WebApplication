using FluentValidation;
using WebApplication.Models.Entities;

namespace WebApplication.Validator
{
    public class UserValidatior : AbstractValidator<Users>
    {

        public UserValidatior()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Tên không được để trống!")
                .MaximumLength(100).WithMessage("Tên tối đa 100 ký tự!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống!");
        }
    }
}
