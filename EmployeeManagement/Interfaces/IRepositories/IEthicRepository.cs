using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface IEthicRepository : IRepository<Ethic>
    {
        Task<bool> IsAnyEthic(string name);
        Task<bool> IsAnyEthic(int id);
    }
}
