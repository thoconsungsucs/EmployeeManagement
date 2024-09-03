using System.Linq.Expressions;

namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        T Delete(T entity);
        Task SaveAsync();
    }
}
