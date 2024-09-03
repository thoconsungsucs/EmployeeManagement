using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface IDistrictRepository : IRepository<District>
    {
        Task<bool> IsDistrictExists(string name);
        Task<bool> IsDistrictExists(int id);
    }
}
