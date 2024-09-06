using EmployeeManagement.Models;
using FluentValidation;

namespace EmployeeManagement.Validation
{
    public class DiplomaValidator : AbstractValidator<Diploma>
    {
        public DiplomaValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Diploma name is required")
                .MaximumLength(50).WithMessage("Diploma name must not exceed 50 characters")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Diploma name must contain only letters, numbers and spaces");
            RuleFor(d => d.IssuedDate).NotEmpty().WithMessage("Start date is required");
            RuleFor(d => d.ExpiryDate).NotEmpty().WithMessage("End date is required");
            RuleFor(d => d.ExpiryDate).GreaterThanOrEqualTo(d => d.IssuedDate).WithMessage("End date must be greater than or equal to start date");
        }
    }
}
