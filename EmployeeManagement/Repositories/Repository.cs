using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagement.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public DbSet<T> DbSet { get; private set; }

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }
        public IQueryable<T> GetAllAsync()
        {
            return DbSet;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {

            var query = DbSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            DbSet.Update(entity);
            return entity;
        }
        public T Delete(T entity)
        {
            DbSet.Remove(entity);
            return entity;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
