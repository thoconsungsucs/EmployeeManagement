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

        public async Task<bool> IsAnyCity(string name, int id)
        {
            return await _context.Cities.AnyAsync(c => c.Name.ToLower() == name.ToLower() && c.CityId != id);
        }

        public async Task<bool> IsAnyCity(int id)
        {
            return await _context.Cities.AnyAsync(c => c.CityId == id);
        }

    }
}
