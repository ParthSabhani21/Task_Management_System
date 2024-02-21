using FluentValidation;
using TaskManagement.Core.Domain.RequestModel;

namespace TaskManagement.Core.Domain.Validation;

public class UserValidation : AbstractValidator<UserRequestModel>
{
    public UserValidation()
    {
        RuleFor(u => u.UserName).NotEmpty().MinimumLength(2);
        RuleFor(u => u.Email).NotEmpty().Matches("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$").WithMessage("Please Enter a Valid Email.");
        RuleFor(u => u.PhoneNumber).NotEmpty().Length(10).WithMessage("Please Enter a Valid PhoneNumber.");
        RuleFor(u => u.Designation).NotEmpty();
        RuleFor(u => u.Gender).NotEmpty();
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.");
    }
}
