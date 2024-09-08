using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class WardService : IWardService
    {
        private readonly IWardRepository _wardRepository;
        private readonly IValidator<Ward> _wardValidator;
        private readonly IDistrictRepository _districtRepository;

        public WardService(IWardRepository WardRepository, IValidator<Ward> WardValidator, IDistrictRepository districtRepository)
        {
            _wardRepository = WardRepository;
            _wardValidator = WardValidator;
            _districtRepository = districtRepository;
        }

        public async Task<ValidationResult> ValidateWard(Ward ward)
        {
            var validationResult = await _wardValidator.ValidateAsync(ward);
            var isAnyDistrict = await _districtRepository.IsAnyDistrict(ward.DistrictId);

            if (!isAnyDistrict)
            {
                validationResult.Errors.Add(new ValidationFailure("District", SD.ValidationMessages.WardMessage.DistrictInvalid));
            }

            return validationResult;
        }

        public async Task<ValidationResult> AddAsync(Ward ward)
        {
            var validationResult = await ValidateWard(ward);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            await _wardRepository.AddAsync(ward);
            await _wardRepository.SaveAsync();
            return validationResult;
        }

        public async Task<Ward> DeleteAsync(Ward ward)
        {
            _wardRepository.Delete(ward);
            await _wardRepository.SaveAsync();
            return ward;
        }

        public async Task<IEnumerable<Ward>> GetAllAsync(Filter filter = null)
        {
            var wards = _wardRepository.GetAllAsync().Select(d => new Ward
            {
                WardId = d.WardId,
                Name = d.Name,
                District = new District
                {
                    Name = d.District.Name,
                    City = new City
                    {
                        Name = d.District.City.Name
                    }
                }
            });
            if (filter == null)
            {
                return await wards.ToArrayAsync();
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                wards = wards.Where(c => c.Name.Contains(filter.Name));
            }
            return await wards.Take(filter.PageSize)
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Ward>> GetAllAsync(int districtId)
        {
            return await _wardRepository.GetAllAsync().Where(e => e.DistrictId == districtId).ToArrayAsync();
        }

        public async Task<Ward> GetByIdAsync(int id)
        {
            return await _wardRepository.GetAsync(e => e.WardId == id, includeProperties: "District");
        }

        public async Task<ValidationResult> UpdateAsync(Ward ward)
        {
            var validationResult = await ValidateWard(ward);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            _wardRepository.Update(ward);
            await _wardRepository.SaveAsync();
            return validationResult;
        }
    }
}
