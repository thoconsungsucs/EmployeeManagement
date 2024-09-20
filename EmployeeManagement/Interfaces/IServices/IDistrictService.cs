using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface IDistrictService
    {
        Task<IEnumerable<DistrictModel>> GetAllFilterAsync(Filter? filter = null);
        Task<IEnumerable<DistrictModel>> GetAllAsync(int cityId);
        Task<IEnumerable<DistrictModel>> GetAllAsync();
        Task<DistrictModel> GetByIdAsync(int id);
        Task<ValidationResult> AddAsync(DistrictModel entity);
        Task<ValidationResult> UpdateAsync(DistrictModel entity);
        Task<ValidationResult> DeleteAsync(int districtId);
    }
}
