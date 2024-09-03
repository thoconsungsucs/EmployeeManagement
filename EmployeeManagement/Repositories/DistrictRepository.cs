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
        public async Task<bool> IsDistrictExists(string name)
        {
            return await _context.Districts.AnyAsync(d => d.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> IsDistrictExists(int id)
        {
            return await _context.Districts.AnyAsync(d => d.DistrictId == id);
        }
    }
}
