using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation;

namespace EmployeeManagement.Validation
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator(ICityRepository cityRepository, IDistrictRepository districtRepository, IWardRepository wardRepository, IEmployeeRepository employeeRepository)
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(SD.ValidationMessages.EmployeeMessage.NameLength)
                .Matches(@"^[a-zA-Z\s]+$").WithMessage(SD.ValidationMessages.EmployeeMessage.NameInvalid)
                .MinimumLength(2).WithMessage(SD.ValidationMessages.EmployeeMessage.NameLength)
                .MaximumLength(50).WithMessage(SD.ValidationMessages.EmployeeMessage.NameLength);
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage(SD.ValidationMessages.EmployeeMessage.DateOfBirthRequired)
                .Must(x => x.Year < System.DateTime.Now.Year - 18).WithMessage(SD.ValidationMessages.EmployeeMessage.DateOfBirthInvalid);
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\d{10}$").WithMessage(SD.ValidationMessages.EmployeeMessage.PhoneNumberInvalid).When(x => x.PhoneNumber != null);

            RuleFor(x => x.Diplomas)
                .Must(diplomas => diplomas.All(d => d.IssuedDate < d.ExpiryDate))
                .WithMessage(SD.ValidationMessages.DiplomaMessage.DateInvalid);

        }
    }
}
