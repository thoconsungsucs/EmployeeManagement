using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<bool> IsAnyJob(string name);
        Task<bool> IsAnyJob(int id);
    }
}
