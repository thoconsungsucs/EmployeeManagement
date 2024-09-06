using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public Task<ValidationResult> AddAsync(Job job)
        {
            throw new NotImplementedException();
        }

        public Task<Job> DeleteAsync(Job job)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _jobRepository.GetAllAsync().Select(j => new Job
            {
                JobId = j.JobId,
                Title = j.Title,
            }).ToArrayAsync();
        }

        public Task<Job> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> UpdateAsync(Job job)
        {
            throw new NotImplementedException();
        }
    }
}
