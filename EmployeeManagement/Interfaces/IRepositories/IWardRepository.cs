using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface IWardRepository : IRepository<Ward>
    {
        Task<bool> IsWardExists(string name);
        Task<bool> IsWardExists(int id);
        Task<bool> DoesBelongToDistrict(int districtId, int wardId);
    }
}
