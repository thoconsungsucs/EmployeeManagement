﻿using EmployeeManagement.Interfaces.IRepositories;
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
        public async Task<IEnumerable<City>> GetAllAsync(CityFilter cityFilter = null)
        {
            var cities = _cityRepository.GetAllAsync();
            if (cityFilter == null)
            {
                return await cities.ToArrayAsync();
            }
            if (!string.IsNullOrEmpty(cityFilter.Name))
            {
                cities = cities.Where(c => c.Name.Contains(cityFilter.Name));
            }
            return await cities.Take(cityFilter.PageSize)
                .Skip(cityFilter.PageSize * (cityFilter.PageNumber - 1))
                .ToArrayAsync();
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }

        public async Task<ValidationResult> AddAsync(City city)
        {
            var validationResult = await _cityValidator.ValidateAsync(city);
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
            var validationResult = await _cityValidator.ValidateAsync(city);
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
