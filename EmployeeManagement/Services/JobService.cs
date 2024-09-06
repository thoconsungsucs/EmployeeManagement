using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class EthicService : IEthicService
    {
        private readonly IEthicRepository _ethicRepository;

        public EthicService(IEthicRepository ethicRepository)
        {
            _ethicRepository = ethicRepository;
        }

        public Task<ValidationResult> AddAsync(Ethic Ethic)
        {
            throw new NotImplementedException();
        }

        public Task<Ethic> DeleteAsync(Ethic Ethic)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Ethic>> GetAllAsync()
        {
            return await _ethicRepository.GetAllAsync().Select(j => new Ethic
            {
                EthicId = j.EthicId,
                Name = j.Name,
            }).ToArrayAsync();
        }

        public Task<Ethic> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> UpdateAsync(Ethic Ethic)
        {
            throw new NotImplementedException();
        }
    }
}
