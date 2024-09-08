using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<bool> IsAnyCity(string name, int id);
        Task<bool> IsAnyCity(int id);
    }
}
