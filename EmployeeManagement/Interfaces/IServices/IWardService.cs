using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface IWardService
    {
        Task<IEnumerable<WardModel>> GetAllFilterAsync(Filter filter = null);
        Task<IEnumerable<WardModel>> GetAllAsync(int districtId);
        Task<IEnumerable<WardModel>> GetAllAsync();
        Task<WardModel> GetByIdAsync(int id);
        Task<ValidationResult> AddAsync(WardModel ward);
        Task<ValidationResult> UpdateAsync(WardModel ward);
        Task<ValidationResult> DeleteAsync(WardModel ward);
    }
}
