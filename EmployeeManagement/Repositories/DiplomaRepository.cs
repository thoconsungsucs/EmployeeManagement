using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class DiplomaRepository : Repository<Diploma>, IDiplomaRepository
    {
        private readonly ApplicationDbContext _context;
        public DiplomaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Diploma>> GetAllByEmployeeId(int employeeId)
        {
            return await _context.Diplomas.Where(d => d.EmployeeId == employeeId).ToArrayAsync();
        }

    }
}
