namespace EmployeeManagement.Interfaces.IRepositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        T Delete(T entity);
        Task SaveAsync();
    }
}
