using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        private readonly ApplicationDbContext _context;
        public DistrictRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> IsAnyDistrict(string name)
        {
            return await _context.Districts.AnyAsync(d => d.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> IsAnyDistrict(int id)
        {
            return await _context.Districts.AnyAsync(d => d.DistrictId == id);
        }

        public async Task<bool> DoesBelongToCity(int cityId, int districtId)
        {
            return await _context.Districts.AnyAsync(d => d.CityId == cityId && d.DistrictId == districtId);
        }
    }
}
