using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> GetAllFilterAsync(Filter? filter);
        Task<EmployeeModel> GetByIdAsync(int id);
        Task<ValidationResult> AddAsync(EmployeeModel employee);
        Task<ValidationResult> UpdateAsync(EmployeeModel employee);
        Task<ValidationResult> DeleteAsync(int id);
        Task<byte[]> ExportEmployee();
        Task<List<string>> ImportEmployees(IFormFile file);
    }
}
