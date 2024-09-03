using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class WardRepository : Repository<Ward>, IWardRepository
    {
        private readonly ApplicationDbContext _context;
        public WardRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> IsWardExists(string name)
        {
            return await _context.Wards.AnyAsync(w => w.Name.ToLower() == name.ToLower());
        }
    }
}
