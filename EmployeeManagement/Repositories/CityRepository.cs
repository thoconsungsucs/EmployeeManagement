using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _context;
        public CityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsCityExists(string name)
        {
            var isExist = await _context.Cities.AnyAsync(c => c.Name.ToLower() == name.ToLower());
            return await _context.Cities.AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> IsCityExists(int id)
        {
            return await _context.Cities.AnyAsync(c => c.CityId == id);
        }
    }
}
