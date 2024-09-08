using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IValidator<District> _districtValidator;

        public DistrictService(IDistrictRepository districtRepository, IValidator<District> districtValidator, ICityRepository cityRepository)
        {
            _districtRepository = districtRepository;
            _districtValidator = districtValidator;
            _cityRepository = cityRepository;
        }

        public async Task<ValidationResult> ValidateDistrict(District entity)
        {
            var validationResult = await _districtValidator.ValidateAsync(entity);
            var isNameDuplicate = await _districtRepository.IsAnyDistrict(entity.Name, entity.DistrictId);
            var isAnyCity = await _cityRepository.IsAnyCity(entity.CityId);

            if (isNameDuplicate)
            {
                validationResult.Errors.Add(new ValidationFailure("Name", SD.ValidationMessages.DistrictMessage.NameUnique));
            }
            if (!isAnyCity)
            {
                validationResult.Errors.Add(new ValidationFailure("City", SD.ValidationMessages.DistrictMessage.CityInvalid));
            }
            return validationResult;
        }

        public async Task<ValidationResult> AddAsync(District entity)
        {
            var validationResult = await ValidateDistrict(entity);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            await _districtRepository.AddAsync(entity);
            await _districtRepository.SaveAsync();
            return validationResult;
        }

        public async Task<District> DeleteAsync(District entity)
        {
            _districtRepository.Delete(entity);
            await _districtRepository.SaveAsync();
            return entity;
        }

        public async Task<IEnumerable<District>> GetAllAsync(Filter? filter = null)
        {
            var districts = _districtRepository.GetAllAsync().Select(d => new District
            {
                DistrictId = d.DistrictId,
                Name = d.Name,
                City = new City
                {
                    Name = d.City.Name
                }
            });
            if (filter == null)
            {
                return await districts.ToArrayAsync();
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                districts = districts.Where(c => c.Name.Contains(filter.Name));
            }
            return await districts.Take(filter.PageSize)
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .ToArrayAsync();
        }

        public async Task<IEnumerable<District>> GetAllAsync(int cityId)
        {
            var districts = await _districtRepository.GetAllAsync().Where(d => d.CityId == cityId).Select(d => new District
            {
                DistrictId = d.DistrictId,
                Name = d.Name,
            }).ToArrayAsync();
            return districts;
        }

        public async Task<District> GetByIdAsync(int id)
        {
            return await _districtRepository.GetAsync(e => e.DistrictId == id, includeProperties: "City");
        }

        public async Task<ValidationResult> UpdateAsync(District entity)
        {
            var validationResult = await ValidateDistrict(entity);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            _districtRepository.Update(entity);
            await _districtRepository.SaveAsync();
            return validationResult;
        }
    }
}
