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
                .MaximumLength(50).WithMessage(SD.ValidationMessages.DistrictMessage.NameLength);

            RuleFor(d => d.CityId)
                .NotEmpty().WithMessage(SD.ValidationMessages.DistrictMessage.CityRequired);


        }
    }
}
