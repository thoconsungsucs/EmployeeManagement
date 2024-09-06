using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation;

namespace EmployeeManagement.Validation
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator(ICityRepository cityRepository)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(SD.ValidationMessages.CityMessage.NameRequired)
                .MinimumLength(2).WithMessage(SD.ValidationMessages.CityMessage.NameLength)
                .MaximumLength(50).WithMessage(SD.ValidationMessages.CityMessage.NameLength)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage(SD.ValidationMessages.CityMessage.NameRegex) // Match only letters and spaces
                .MustAsync(async (name, _) =>
                {
                    return !(await cityRepository.IsAnyCity(name));
                }).WithMessage(SD.ValidationMessages.CityMessage.NameUnique);
        }
    }
}
