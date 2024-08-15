using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class CityService : ICityService
    {
        private readonly IRepository<City> _cityRepository;
        public CityService(IRepository<City> cityRepository)
        {
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

        public async Task<City> AddAsync(City city)
        {
            await _cityRepository.AddAsync(city);
            await _cityRepository.SaveAsync();
            return city;
        }

        public async Task<City> DeleteAsync(City city)
        {
            _cityRepository.Delete(city);
            await _cityRepository.SaveAsync();
            return city;
        }


        public async Task<City> UpdateAsync(City entity)
        {
            _cityRepository.Update(entity);
            await _cityRepository.SaveAsync();
            return entity;
        }
    }
}
