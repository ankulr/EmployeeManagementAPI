using EmployeeManagement.DTOs;
using FluentValidation;

namespace EmployeeManagement.Validators
{
    public class CreateEmployeeDTOValidator : AbstractValidator<CreateEmployeeDTO>
    {
        public CreateEmployeeDTOValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid  email format ");
            RuleFor(x => x.Salary).GreaterThan(0).WithMessage("Salary must be greater than zero");
            RuleFor(x => x.DepartmentId).GreaterThan(0).WithMessage("DepartmentId must be greater than zero");
        }
    }
}
