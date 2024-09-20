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
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IValidator<DistrictModel> _districtValidator;
        private readonly IMapper _mapper;

        public DistrictService(
            IDistrictRepository districtRepository,
            IValidator<DistrictModel> districtValidator,
            ICityRepository cityRepository,
            IMapper mapper
            )
        {
            _districtRepository = districtRepository;
            _districtValidator = districtValidator;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult> ValidateDistrict(DistrictModel entity)
        {
            var validationResult = await _districtValidator.ValidateAsync(entity);
            var isNameDuplicate = await _districtRepository.IsAnyDistrict(entity.DistrictName, entity.DistrictId);
            var isAnyCity = await _cityRepository.IsAnyCity(entity.CityId);

            if (isNameDuplicate)
            {
                validationResult.Errors.Add(new ValidationFailure("Name", SD.ValidationMessages.DistrictMessage.NameUnique));
            }
            if (!isAnyCity)
            {
                validationResult.Errors.Add(new ValidationFailure("City", SD.ValidationMessages.DistrictMessage.CityInvalid));
            }
            return validationResult;
        }

        public async Task<ValidationResult> AddAsync(DistrictModel districtModel)
        {
            var validationResult = await ValidateDistrict(districtModel);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            var district = _mapper.Map<District>(districtModel);
            await _districtRepository.AddAsync(district);
            await _districtRepository.SaveAsync();
            return validationResult;
        }

        public async Task<ValidationResult> DeleteAsync(int districtId)
        {
            var validationResult = new ValidationResult();
            var district = await _districtRepository.GetAsync(d => d.DistrictId == districtId);
            if (district == null)
            {
                validationResult.Errors.Add(new ValidationFailure("District", SD.ValidationMessages.DistrictMessage.DistrictInvalid));
                return validationResult;
            }
            _districtRepository.Delete(district);
            await _districtRepository.SaveAsync();
            return validationResult;
        }

        public IQueryable<District> GetAllIncludeCityName()
        {
            var districts = _districtRepository.GetAllAsync().Select(d => new District
            {
                DistrictId = d.DistrictId,
                Name = d.Name,
                City = new City
                {
                    Name = d.City.Name
                }
            });
            return districts;
        }

        public async Task<IEnumerable<District>> FilterDistrict(IQueryable<District> districts, Filter? filter)
        {
            if (filter == null)
            {
                return await districts.ToArrayAsync();
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                districts = districts.Where(c => c.Name.Contains(filter.Name));
            }
            // Paging
            // Query tree just passed when ToArrayAsync() is called
            return await districts.Take(filter.PageSize)
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .ToArrayAsync();
        }

        public async Task<IEnumerable<DistrictModel>> GetAllFilterAsync(Filter? filter = null)
        {
            var districts = GetAllIncludeCityName();

            var filteredDistricts = await FilterDistrict(districts, filter);
            return _mapper.Map<IEnumerable<DistrictModel>>(filteredDistricts);
        }

        public async Task<IEnumerable<DistrictModel>> GetAllAsync(int cityId)
        {
            var districts = await _districtRepository.GetAllAsync().Where(d => d.CityId == cityId).Select(d => new District
            {
                DistrictId = d.DistrictId,
                Name = d.Name,
            }).ToArrayAsync();
            return _mapper.Map<IEnumerable<DistrictModel>>(districts);
        }

        public async Task<DistrictModel> GetByIdAsync(int id)
        {
            var distrct = await _districtRepository.GetAsync(e => e.DistrictId == id, includeProperties: "City");
            return _mapper.Map<DistrictModel>(distrct);
        }

        public async Task<ValidationResult> UpdateAsync(DistrictModel entity)
        {
            var validationResult = await ValidateDistrict(entity);
            var district = await _districtRepository.GetAsync(d => d.DistrictId == entity.DistrictId);
            if (district == null)
            {
                validationResult.Errors.Add(new ValidationFailure("District", SD.ValidationMessages.DistrictMessage.DistrictInvalid));
                return validationResult;
            }
            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            _mapper.Map(entity, district);
            _districtRepository.Update(district);
            await _districtRepository.SaveAsync();
            return validationResult;
        }

        public async Task<IEnumerable<DistrictModel>> GetAllAsync()
        {
            var districts = await _districtRepository.GetAllAsync().ToArrayAsync();
            return _mapper.Map<IEnumerable<DistrictModel>>(districts);
        }
    }
}
