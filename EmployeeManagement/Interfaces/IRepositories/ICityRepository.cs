using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<bool> IsCityExists(string name);
    }
}
