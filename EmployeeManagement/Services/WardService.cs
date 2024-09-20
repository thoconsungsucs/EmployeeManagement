using AutoMapper;
using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class WardService : IWardService
    {
        private readonly IWardRepository _wardRepository;
        private readonly IValidator<WardModel> _wardValidator;
        private readonly IDistrictRepository _districtRepository;
        private readonly IMapper _mapper;

        public WardService(
            IWardRepository WardRepository,
            IValidator<WardModel> WardValidator,
            IDistrictRepository districtRepository,
            IMapper mapper)
        {
            _wardRepository = WardRepository;
            _wardValidator = WardValidator;
            _districtRepository = districtRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult> ValidateWard(WardModel ward)
        {
            var validationResult = await _wardValidator.ValidateAsync(ward);
            var isAnyDistrict = await _districtRepository.IsAnyDistrict(ward.DistrictId);

            if (!isAnyDistrict)
            {
                validationResult.Errors.Add(new ValidationFailure("District", SD.ValidationMessages.WardMessage.DistrictInvalid));
            }

            return validationResult;
        }

        public async Task<ValidationResult> AddAsync(WardModel wardModel)
        {
            var validationResult = await ValidateWard(wardModel);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            var ward = _mapper.Map<Ward>(wardModel);
            await _wardRepository.AddAsync(ward);
            await _wardRepository.SaveAsync();
            return validationResult;
        }

        public async Task<ValidationResult> DeleteAsync(WardModel wardModel)
        {
            var ward = await _wardRepository.GetAsync(w => w.WardId == wardModel.WardId);
            var validationResult = new ValidationResult();
            if (ward == null)
            {
                validationResult.Errors.Add(new ValidationFailure("Ward", SD.ValidationMessages.WardMessage.NotFound));
                return validationResult;
            }
            _wardRepository.Delete(ward);
            await _wardRepository.SaveAsync();
            return validationResult;
        }


        public IQueryable<Ward> GetAllIncludeCityDistrictName()
        {
            var wards = _wardRepository.GetAllAsync().Select(d => new Ward
            {
                WardId = d.WardId,
                Name = d.Name,
                District = new District
                {
                    Name = d.District.Name,
                    City = new City
                    {
                        Name = d.District.City.Name
                    }
                }
            });
            return wards;
        }

        public async Task<IEnumerable<Ward>> FilterWard(IQueryable<Ward> wards, Filter? filter = null)
        {
            if (filter == null)
            {
                return await wards.ToArrayAsync();
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                wards = wards.Where(c => c.Name.Contains(filter.Name));
            }
            return await wards.Take(filter.PageSize)
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .ToArrayAsync();
        }

        public async Task<IEnumerable<WardModel>> GetAllFilterAsync(Filter? filter = null)
        {
            var wards = GetAllIncludeCityDistrictName();

            var filteredWard = await FilterWard(wards, filter);
            return _mapper.Map<IEnumerable<WardModel>>(filteredWard);
        }

        public async Task<IEnumerable<WardModel>> GetAllAsync(int districtId)
        {
            var wards = await _wardRepository.GetAllAsync().Where(e => e.DistrictId == districtId).ToArrayAsync();
            return _mapper.Map<IEnumerable<WardModel>>(wards);
        }

        public async Task<WardModel> GetByIdAsync(int id)
        {
            var ward = await _wardRepository.GetAsync(e => e.WardId == id, includeProperties: "District");
            return _mapper.Map<WardModel>(ward);
        }

        public async Task<ValidationResult> UpdateAsync(WardModel wardModel)
        {
            var validationResult = await ValidateWard(wardModel);
            var ward = await _wardRepository.GetAsync(d => d.WardId == wardModel.WardId);
            if (ward == null)
            {
                validationResult.Errors.Add(new ValidationFailure("Ward", SD.ValidationMessages.WardMessage.NotFound));
                return validationResult;
            }
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            _mapper.Map(wardModel, ward);
            _wardRepository.Update(ward);
            await _wardRepository.SaveAsync();
            return validationResult;
        }

        public async Task<IEnumerable<WardModel>> GetAllAsync()
        {
            var wards = await _wardRepository
                .GetAllAsync()
                .Include(w => w.District)
                    .ThenInclude(w => w.City)
                .ToArrayAsync();
            return _mapper.Map<IEnumerable<WardModel>>(wards);
        }
    }
}
