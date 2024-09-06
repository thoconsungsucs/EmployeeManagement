using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class EthicRepository : Repository<Ethic>, IEthicRepository
    {
        private readonly ApplicationDbContext _context;
        public EthicRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsAnyEthic(string name)
        {
            var isExist = await _context.Ethics.AnyAsync(e => e.Name.ToLower() == name.ToLower());
            return await _context.Ethics.AnyAsync(e => e.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> IsAnyEthic(int id)
        {
            return await _context.Ethics.AnyAsync(e => e.EthicId == id);
        }
    }
}
