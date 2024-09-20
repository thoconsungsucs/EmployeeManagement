using AutoMapper;
using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IValidator<CityModel> _cityValidator;
        private readonly IMapper _mapper;
        public CityService(ICityRepository cityRepository, IValidator<CityModel> cityValiadtor, IMapper mapper)
        {
            _cityValidator = cityValiadtor;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<City>> FilterCity(IQueryable<City> cities, Filter? filter = null)
        {
            if (filter == null)
            {
                return await cities.ToArrayAsync();
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                cities = cities.Where(c => c.Name.Contains(filter.Name));
            }
            // Paging
            // Query tree just passed when ToArrayAsync() is called
            return await cities.Take(filter.PageSize)
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .ToArrayAsync();
        }

        public async Task<IEnumerable<CityModel>> GetAllFilterAsync(Filter? filter = null)
        {
            // City is IQueryable
            var cities = _cityRepository.GetAllAsync();
            var filteredCities = await FilterCity(cities, filter);
            return _mapper.Map<IEnumerable<CityModel>>(filteredCities);
        }

        public async Task<CityModel> GetByIdAsync(int id)
        {
            var city = await _cityRepository.GetAsync(e => e.CityId == id);
            return _mapper.Map<CityModel>(city);
        }

        public async Task<ValidationResult> ValidateCity(CityModel cityModel)
        {
            var validationResult = await _cityValidator.ValidateAsync(cityModel);
            var isAnyCity = await _cityRepository.IsAnyCity(cityModel.Name, cityModel.CityId);
            if (isAnyCity)
            {
                validationResult.Errors.Add(new ValidationFailure("Name", SD.ValidationMessages.CityMessage.NameUnique));
            }
            return validationResult;
        }

        public async Task<ValidationResult> AddAsync(CityModel cityModel)
        {
            var validationResult = await ValidateCity(cityModel);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            var city = _mapper.Map<City>(cityModel);
            await _cityRepository.AddAsync(city);
            await _cityRepository.SaveAsync();
            return validationResult;
        }

        public async Task<ValidationResult> DeleteAsync(int id)
        {
            var city = await _cityRepository.GetAsync(e => e.CityId == id);
            var validationResult = new ValidationResult();
            if (city == null)
            {
                validationResult.Errors.Add(new ValidationFailure("City", SD.ValidationMessages.CityMessage.CityInvalid));
                return validationResult;
            }
            _cityRepository.Delete(city);
            await _cityRepository.SaveAsync();
            return validationResult;
        }


        public async Task<ValidationResult> UpdateAsync(CityModel cityModel)
        {
            var validationResult = await ValidateCity(cityModel);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            var city = await _cityRepository.GetAsync(c => c.CityId == cityModel.CityId);
            if (city == null)
            {
                validationResult.Errors.Add(new ValidationFailure("CityId", "City not found"));
                return validationResult;
            }
            _mapper.Map(cityModel, city);
            _cityRepository.Update(city);
            await _cityRepository.SaveAsync();
            return validationResult;
        }

        public async Task<IEnumerable<CityModel>> GetAllAsync()
        {
            var cities = await _cityRepository.GetAllAsync().ToArrayAsync();
            return _mapper.Map<IEnumerable<CityModel>>(cities);
        }
    }
}
