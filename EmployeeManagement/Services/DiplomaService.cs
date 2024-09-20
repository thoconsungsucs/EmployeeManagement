using AutoMapper;
using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using FluentValidation;
using FluentValidation.Results;

namespace EmployeeManagement.Services
{
    public class DiplomaService : IDiplomaService
    {
        private readonly IDiplomaRepository _diplomaRepository;
        private readonly IValidator<DiplomaModel> _diplomaValidator;
        private readonly IMapper _mapper;
        public DiplomaService(IDiplomaRepository diplomaRepository, IValidator<DiplomaModel> diplomaValidator, IMapper mapper)
        {
            _diplomaRepository = diplomaRepository;
            _diplomaValidator = diplomaValidator;
            _mapper = mapper;
        }

        public async Task<ValidationResult> AddAsync(DiplomaModel diplomaModel)
        {
            var validationResult = await _diplomaValidator.ValidateAsync(diplomaModel);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            var diplomaNumber = _diplomaRepository.GetAllByEmployeeId(diplomaModel.EmployeeId).Result.Count();
            if (diplomaNumber >= SD.NumberOfDiploma)
            {
                validationResult.Errors.Add(new ValidationFailure("DiplomaNumber", $"Employee can only have {SD.NumberOfDiploma} diplomaModel"));
                return validationResult;
            }
            var diplopma = _mapper.Map<Diploma>(diplomaModel);
            await _diplomaRepository.AddAsync(diplopma);
            await _diplomaRepository.SaveAsync();
            return validationResult;
        }

        public async Task<ValidationResult> DeleteAsync(int diplomaId)
        {
            var validationResult = new ValidationResult();
            var diploma = await _diplomaRepository.GetAsync(d => d.DiplomaId == diplomaId);
            if (diploma == null)
            {
                validationResult.Errors.Add(new ValidationFailure("Diploma", "Diploma not found"));
                return validationResult;
            }
            _diplomaRepository.Delete(diploma);
            await _diplomaRepository.SaveAsync();
            return validationResult;
        }

        public async Task<IEnumerable<DiplomaModel>> GetAllByEmployeeIdAsync(int id)
        {
            var diplomas = await _diplomaRepository.GetAllByEmployeeId(id);
            return _mapper.Map<IEnumerable<DiplomaModel>>(diplomas);
        }

        public async Task<DiplomaModel> GetDiplomaById(int id)
        {
            var diploma = await _diplomaRepository
                .GetAsync(d => d.DiplomaId == id);

            return _mapper.Map<DiplomaModel>(diploma);
        }

        public async Task<ValidationResult> UpdateAsync(DiplomaModel diplomaModel
            )
        {
            var validationResult = await _diplomaValidator.ValidateAsync(diplomaModel);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            var diploma = await _diplomaRepository.GetAsync(d => d.DiplomaId == diplomaModel.DiplomaId);
            if (diploma == null)
            {
                validationResult.Errors.Add(new ValidationFailure("Diploma", "Diploma not found"));
                return validationResult;
            }
            _mapper.Map(diplomaModel, diploma);
            _diplomaRepository.Update(diploma);
            await _diplomaRepository.SaveAsync();
            return validationResult;
        }
    }
}
