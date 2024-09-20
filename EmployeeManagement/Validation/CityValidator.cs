using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using FluentValidation;

namespace EmployeeManagement.Validation
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        public CityValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(SD.ValidationMessages.CityMessage.NameRequired)
                .Length(SD.MinimumNameLength, SD.MaximumNameLength).WithMessage(SD.ValidationMessages.CityMessage.NameLength)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage(SD.ValidationMessages.CityMessage.NameRegex); // Match only letters and spaces

        }
    }
}
