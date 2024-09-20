using EmployeeManagement.ModelViews;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface IDiplomaService
    {
        Task<DiplomaModel> GetDiplomaById(int id);
        Task<IEnumerable<DiplomaModel>> GetAllByEmployeeIdAsync(int id);
        Task<ValidationResult> UpdateAsync(DiplomaModel diploma);
        Task<ValidationResult> DeleteAsync(int diplomaId);
        Task<ValidationResult> AddAsync(DiplomaModel diploma);
    }
}
