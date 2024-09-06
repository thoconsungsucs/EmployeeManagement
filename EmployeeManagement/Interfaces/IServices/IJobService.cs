using EmployeeManagement.Models;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAllAsync();
        Task<Job> GetByIdAsync(int id);
        Task<ValidationResult> AddAsync(Job job);
        Task<ValidationResult> UpdateAsync(Job job);
        Task<Job> DeleteAsync(Job job);
    }
}
