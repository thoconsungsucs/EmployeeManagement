using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<bool> IsAnyCity(string name);
        Task<bool> IsAnyCity(int id);
    }
}
