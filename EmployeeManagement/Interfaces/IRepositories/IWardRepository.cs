using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface IWardRepository : IRepository<Ward>
    {
        public Task<bool> IsWardExists(string name);
    }
}
