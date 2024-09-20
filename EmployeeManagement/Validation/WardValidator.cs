using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using FluentValidation;

namespace EmployeeManagement.Validation
{
    public class WardValidator : AbstractValidator<WardModel>
    {
        public WardValidator(IDistrictRepository districtRepository)
        {
            RuleFor(x => x.WardName)
                .Length(SD.MinimumNameLength, SD.MaximumNameLength).WithMessage(SD.ValidationMessages.WardMessage.NameLength);
            RuleFor(x => x.DistrictId)
                .NotEmpty().WithMessage(SD.ValidationMessages.WardMessage.DistrictRequired);

        }
    }
}
