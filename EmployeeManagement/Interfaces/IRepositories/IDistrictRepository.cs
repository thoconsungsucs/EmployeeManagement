using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface IDistrictRepository : IRepository<District>
    {
        Task<bool> IsAnyDistrict(string name, int id);
        Task<bool> IsAnyDistrict(int id);
        Task<bool> DoesBelongToCity(int cityId, int districtId);
    }
}
