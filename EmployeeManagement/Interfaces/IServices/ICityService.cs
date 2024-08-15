using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllAsync(CityFilter cityFilter = null);
        Task<City> GetByIdAsync(int id);
        Task<City> AddAsync(City entity);
        Task<City> UpdateAsync(City entity);
        Task<City> DeleteAsync(City entity);
    }
}
