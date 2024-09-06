using EmployeeManagement.Models;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface IDiplomaService
    {
        Task<Diploma> GetDiplomaById(int id);
        Task<IEnumerable<Diploma>> GetAllByEmployeeIdAsync(int id);
        Task<ValidationResult> UpdateAsync(Diploma diploma);
        Task DeleteAsync(Diploma diploma);
        Task<ValidationResult> AddAsync(Diploma diploma);
    }
}
