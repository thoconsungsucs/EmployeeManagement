using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class DiplomaService : IDiplomaService
    {
        private readonly IDiplomaRepository _diplomaRepository;
        private readonly IValidator<Diploma> _diplomaValidator;
        public DiplomaService(IDiplomaRepository diplomaRepository, IValidator<Diploma> diplomaValidator)
        {
            _diplomaRepository = diplomaRepository;
            _diplomaValidator = diplomaValidator;
        }

        public async Task<ValidationResult> AddAsync(Diploma diploma)
        {
            var validationResult = await _diplomaValidator.ValidateAsync(diploma);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            var diplomaNumber = _diplomaRepository.GetAllByEmployeeId(diploma.EmployeeId).Result.Count();
            if (diplomaNumber >= 3)
            {
                validationResult.Errors.Add(new ValidationFailure("DiplomaNumber", "Employee can only have 3 diplomas"));
                return validationResult;
            }

            await _diplomaRepository.AddAsync(diploma);
            await _diplomaRepository.SaveAsync();
            return validationResult;
        }

        public async Task DeleteAsync(Diploma diploma)
        {
            _diplomaRepository.Delete(diploma);
            await _diplomaRepository.SaveAsync();
        }

        public async Task<IEnumerable<Diploma>> GetAllByEmployeeIdAsync(int id)
        {
            return await _diplomaRepository.GetAllAsync().Where(d => d.EmployeeId == id).ToListAsync();
        }

        public async Task<Diploma> GetDiplomaById(int id)
        {
            return await _diplomaRepository.GetAsync(d => d.DiplomaId == id);
        }

        public async Task<ValidationResult> UpdateAsync(Diploma diplomas)
        {
            var validationResult = await _diplomaValidator.ValidateAsync(diplomas);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            _diplomaRepository.Update(diplomas);
            await _diplomaRepository.SaveAsync();
            return validationResult;
        }
    }
}
