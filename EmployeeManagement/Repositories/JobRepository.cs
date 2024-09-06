using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly ApplicationDbContext _context;
        public JobRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsAnyJob(string title)
        {
            var isExist = await _context.Jobs.AnyAsync(j => j.Title.ToLower() == title.ToLower());
            return await _context.Jobs.AnyAsync(j => j.Title.ToLower() == title.ToLower());
        }

        public async Task<bool> IsAnyJob(int id)
        {
            return await _context.Jobs.AnyAsync(j => j.JobId == id);
        }
    }
}
