using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IValidator<City> _cityValidator;
        public CityService(ICityRepository cityRepository, IValidator<City> cityValiadtor)
        {
            _cityValidator = cityValiadtor;
            _cityRepository = cityRepository;
        }
        public async Task<IEnumerable<City>> GetAllAsync(Filter filter = null)
        {
            var cities = _cityRepository.GetAllAsync();
            if (filter == null)
            {
                return await cities.ToArrayAsync();
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                cities = cities.Where(c => c.Name.Contains(filter.Name));
            }
            return await cities.Take(filter.PageSize)
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .ToArrayAsync();
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await _cityRepository.GetAsync(e => e.CityId == id);
        }

        public async Task<ValidationResult> ValidateCity(City city)
        {
            var validationResult = await _cityValidator.ValidateAsync(city);
            var isAnyCity = await _cityRepository.IsAnyCity(city.Name, city.CityId);
            if (isAnyCity)
            {
                validationResult.Errors.Add(new ValidationFailure("Name", SD.ValidationMessages.CityMessage.NameUnique));
            }
            return validationResult;
        }

        public async Task<ValidationResult> AddAsync(City city)
        {
            var validationResult = await ValidateCity(city);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            await _cityRepository.AddAsync(city);
            await _cityRepository.SaveAsync();
            return validationResult;
        }

        public async Task<City> DeleteAsync(City city)
        {
            _cityRepository.Delete(city);
            await _cityRepository.SaveAsync();
            return city;
        }


        public async Task<ValidationResult> UpdateAsync(City city)
        {
            var validationResult = await ValidateCity(city);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            _cityRepository.Update(city);
            await _cityRepository.SaveAsync();
            return validationResult;
        }
    }
}
