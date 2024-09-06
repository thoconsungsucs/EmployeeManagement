using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface IDiplomaRepository : IRepository<Diploma>
    {
        Task<IEnumerable<Diploma>> GetAllByEmployeeId(int employeeId);
    }
}
