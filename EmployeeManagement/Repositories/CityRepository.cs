using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _context;
        public CityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
