using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation;

namespace EmployeeManagement.Validation
{
    public class WardValidator : AbstractValidator<Ward>
    {
        public WardValidator(IDistrictRepository districtRepository)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(SD.ValidationMessages.WardMessage.NameRequired);
            RuleFor(x => x.Name).MaximumLength(50).WithMessage(SD.ValidationMessages.WardMessage.NameLength);
            RuleFor(x => x.DistrictId)
                .NotEmpty().WithMessage(SD.ValidationMessages.WardMessage.DistrictRequired)
                .MustAsync(async (districtId, _) =>
                {
                    return await districtRepository.IsDistrictExists(districtId);
                }).WithMessage(SD.ValidationMessages.WardMessage.DistrictInvalid);
        }
    }
}
