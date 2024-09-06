using EmployeeManagement.Models;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface IEthicService
    {
        Task<IEnumerable<Ethic>> GetAllAsync();
        Task<Ethic> GetByIdAsync(int id);
        Task<ValidationResult> AddAsync(Ethic Ethic);
        Task<ValidationResult> UpdateAsync(Ethic Ethic);
        Task<Ethic> DeleteAsync(Ethic Ethic);
    }
}
