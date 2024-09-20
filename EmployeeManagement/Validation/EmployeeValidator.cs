using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using FluentValidation;

namespace EmployeeManagement.Validation
{
    public class EmployeeValidator : AbstractValidator<EmployeeModel>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name)
                .Matches(@"^[a-zA-Z\s]+$").WithMessage(SD.ValidationMessages.EmployeeMessage.NameInvalid)
                .Length(SD.MinimumNameLength, SD.MaximumNameLength).WithMessage(SD.ValidationMessages.EmployeeMessage.NameLength);
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage(SD.ValidationMessages.EmployeeMessage.DateOfBirthRequired)
                .Must(x => x.Year < System.DateTime.Now.Year - SD.AgeLimit).WithMessage(SD.ValidationMessages.EmployeeMessage.DateOfBirthInvalid);
            RuleFor(x => x.PhoneNumber)
               .Matches(@"^\d{" + SD.PhoneNumberLength + "}$").WithMessage(SD.ValidationMessages.EmployeeMessage.PhoneNumberInvalid)
                .When(x => x.PhoneNumber != null);

            RuleFor(x => x.Diplomas)
                .Must(diplomas => diplomas.All(d => d.IssuedDate < d.ExpiryDate))
                .WithMessage(SD.ValidationMessages.DiplomaMessage.DateInvalid);

        }
    }
}
