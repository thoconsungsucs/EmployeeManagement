using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<bool> IsCityExists(string name);
        Task<bool> IsCityExists(int id);
    }
}
