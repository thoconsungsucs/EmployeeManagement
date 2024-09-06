using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<bool> IsAnyIdentityId(string identityId);
    }
}
