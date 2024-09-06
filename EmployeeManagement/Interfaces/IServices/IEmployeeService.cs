using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation.Results;

namespace EmployeeManagement.Interfaces.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync(Filter? filter);
        Task<Employee> GetByIdAsync(int id);
        Task<ValidationResult> AddAsync(Employee employee);
        Task<ValidationResult> UpdateAsync(Employee employee);
        Task<Employee> DeleteAsync(Employee employee);
    }
}
