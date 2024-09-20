using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface ICityService
    {
        Task<IEnumerable<CityModel>> GetAllFilterAsync(Filter? filter = null);
        Task<IEnumerable<CityModel>> GetAllAsync();
        Task<CityModel> GetByIdAsync(int id);
        Task<ValidationResult> AddAsync(CityModel entity);
        Task<ValidationResult> UpdateAsync(CityModel entity);
        Task<ValidationResult> DeleteAsync(int id);
    }
}
