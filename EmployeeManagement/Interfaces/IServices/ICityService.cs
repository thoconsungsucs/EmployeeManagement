using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllAsync(Filter filter = null);
        Task<City> GetByIdAsync(int id);
        Task<ValidationResult> AddAsync(City entity);
        Task<ValidationResult> UpdateAsync(City entity);
        Task<City> DeleteAsync(City entity);
    }
}
