using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface IWardService
    {
        Task<IEnumerable<Ward>> GetAllAsync(Filter filter = null);
        Task<IEnumerable<Ward>> GetAllAsync(int districts);
        Task<Ward> GetByIdAsync(int id);
        Task<ValidationResult> AddAsync(Ward ward);
        Task<ValidationResult> UpdateAsync(Ward ward);
        Task<Ward> DeleteAsync(Ward ward);
    }
}
