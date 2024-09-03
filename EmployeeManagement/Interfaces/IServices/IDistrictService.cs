using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> GetAllAsync(Filter filter = null);
        Task<IEnumerable<District>> GetAllAsync(int cityId);
        Task<District> GetByIdAsync(int id);
        Task<ValidationResult> AddAsync(District entity);
        Task<ValidationResult> UpdateAsync(District entity);
        Task<District> DeleteAsync(District entity);
    }
}
