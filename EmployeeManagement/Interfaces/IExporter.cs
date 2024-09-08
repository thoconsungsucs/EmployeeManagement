using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IExporter
    {
        Task<byte[]> ExportEmployees(List<Employee> employees);
    }
}
