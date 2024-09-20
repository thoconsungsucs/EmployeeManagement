using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using FluentValidation;

namespace EmployeeManagement.Validation
{
    public class DistrictValidator : AbstractValidator<DistrictModel>
    {
        public DistrictValidator()
        {
            RuleFor(d => d.DistrictName)
                .Length(SD.MinimumNameLength, SD.MaximumNameLength).WithMessage(SD.ValidationMessages.DistrictMessage.NameLength);

            RuleFor(d => d.CityId)
                .NotEmpty().WithMessage(SD.ValidationMessages.DistrictMessage.CityRequired);


        }
    }
}
