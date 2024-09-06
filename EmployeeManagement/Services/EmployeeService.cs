using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using EmployeeManagement.Ultilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IValidator<Employee> _employeeValidator;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IDiplomaRepository _diplomaRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IValidator<Employee> employeeValidator,
            IDistrictRepository districtRepository,
            IWardRepository wardRepository,
            IDiplomaRepository diplomaRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator;
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
            _diplomaRepository = diplomaRepository;
        }

        public async Task<ValidationResult> AddAsync(Employee employee)
        {
            employee.Age = DateTime.Now.Year - employee.DateOfBirth.Year;

            var validationResult = await _employeeValidator.ValidateAsync(employee);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            var addressValid = await ValidateAddress(employee);
            if (!addressValid)
            {
                validationResult.Errors.Add(new ValidationFailure("Address", "Address validation failed."));
                return validationResult;
            }

            if (await _employeeRepository.IsAnyIdentityId(employee.IdentityId))
            {
                validationResult.Errors.Add(new ValidationFailure("IdentityId", SD.ValidationMessages.EmployeeMessage.IdentityIdInvalid));
                return validationResult;
            }


            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveAsync();

            return validationResult;
        }

        public async Task<Employee> DeleteAsync(Employee employee)
        {
            _employeeRepository.Delete(employee);
            await _employeeRepository.SaveAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(Filter filter = null)
        {
            var employees = _employeeRepository.GetAllAsync()
                .Select(e => new Employee
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    IdentityId = e.IdentityId,
                    PhoneNumber = e.PhoneNumber,
                    DateOfBirth = e.DateOfBirth,
                    Age = e.Age,
                    Job = new Job
                    {
                        Title = e.Job.Title,
                    },
                    Ethic = new Ethic
                    {
                        Name = e.Ethic.Name
                    },
                    Ward = new Ward
                    {
                        Name = e.Ward.Name
                    },
                    District = new District
                    {
                        Name = e.District.Name
                    },
                    City = new City
                    {
                        Name = e.City.Name
                    },
                    Address = e.Address,
                    Diplomas = e.Diplomas.Select(d => new Diploma
                    {
                        DiplomaId = d.DiplomaId,
                        Name = d.Name,
                        IssuedDate = d.IssuedDate,
                        ExpiryDate = d.ExpiryDate
                    }).ToList()
                });

            if (filter == null)
            {
                return await employees.ToArrayAsync();
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                employees = employees.Where(c => c.Name.Contains(filter.Name));
            }
            return await employees.Take(filter.PageSize)
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .ToArrayAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetAsync(e => e.EmployeeId == id, includeProperties: "Diplomas,City,District,Ward,Job,Ethic");
        }

        public async Task<ValidationResult> UpdateAsync(Employee employee)
        {
            var validationResult = await _employeeValidator.ValidateAsync(employee);
            var addressValid = await ValidateAddress(employee);
            if (!addressValid)
            {
                validationResult.Errors.Add(new ValidationFailure("Address", "Address validation failed."));
                return validationResult;
            }

            if (!validationResult.IsValid)
            {
                return validationResult;
            }
            employee.Age = DateTime.Now.Year - employee.DateOfBirth.Year;
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAsync();
            return validationResult;
        }

        public async Task<bool> ValidateAddress(Employee employee)
        {
            var doesBelongToDistrict = await _wardRepository.DoesBelongToDistrict(employee.DistrictId, employee.WardId);
            var doesBelongToCity = await _districtRepository.DoesBelongToCity(employee.CityId, employee.DistrictId);
            return doesBelongToDistrict && doesBelongToCity;
        }

    }
}
