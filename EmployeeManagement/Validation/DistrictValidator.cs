using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation;

namespace EmployeeManagement.Validation
{
    public class DistrictValidator : AbstractValidator<District>
    {
        public DistrictValidator(ICityRepository cityRepository, IDistrictRepository districtRepository)
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage(SD.ValidationMessages.DistrictMessage.NameRequired)
                .MinimumLength(2).WithMessage(SD.ValidationMessages.DistrictMessage.NameLength)
                .MaximumLength(50).WithMessage(SD.ValidationMessages.DistrictMessage.NameLength)
                .MustAsync(async (name, _) =>
                    {
                        return !await districtRepository.IsDistrictExists(name);
                    }).WithMessage(SD.ValidationMessages.DistrictMessage.NameUnique);

            RuleFor(d => d.CityId)
                .NotEmpty().WithMessage("City is required")
                .MustAsync(async (cityId, _) =>
                {
                    return await cityRepository.IsCityExists(cityId);
                }).WithMessage(SD.ValidationMessages.DistrictMessage.CityInvalid);
        }
    }
}
