using ClosedXML.Excel;
using EmployeeManagement.Interfaces;
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
        private readonly IJobRepository _jobRepository;
        private readonly IEthicRepository _ethicRepository;
        private readonly IDiplomaRepository _diplomaRepository;
        private readonly IExporter _exporter;
        private readonly ICityRepository _cityRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IValidator<Employee> employeeValidator,
            IDistrictRepository districtRepository,
            IWardRepository wardRepository,
            IDiplomaRepository diplomaRepository,
            IExporter exporter,
            IJobRepository jobRepository,
            IEthicRepository ethicRepository,
            ICityRepository cityRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator;
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
            _diplomaRepository = diplomaRepository;
            _exporter = exporter;
            _ethicRepository = ethicRepository;
            _jobRepository = jobRepository;
            _cityRepository = cityRepository;
        }

        public async Task<ValidationResult> ValidateEmployee(Employee employee)
        {
            var validationResult = await _employeeValidator.ValidateAsync(employee);
            var doesBelongToDistrict = await _wardRepository.DoesBelongToDistrict(employee.DistrictId, employee.WardId);
            var doesBelongToCity = await _districtRepository.DoesBelongToCity(employee.CityId, employee.DistrictId);
            var isDuplicateIdentityId = !String.IsNullOrEmpty(employee.IdentityId)
                    && await _employeeRepository.IsAnyIdentityId(employee.IdentityId);

            if (isDuplicateIdentityId)
            {
                validationResult.Errors.Add(new ValidationFailure("IdentityId", SD.ValidationMessages.EmployeeMessage.IdentityIdUnique));
            }
            if (!doesBelongToDistrict)
            {
                validationResult.Errors.Add(new ValidationFailure("Ward", SD.ValidationMessages.WardMessage.WardInvalid));
            }
            if (!doesBelongToCity)
            {
                validationResult.Errors.Add(new ValidationFailure("District", SD.ValidationMessages.DistrictMessage.DistrictInvalid));
            }
            return validationResult;
        }

        public async Task<ValidationResult> AddAsync(Employee employee)
        {
            // Update age
            employee.Age = DateTime.Now.Year - employee.DateOfBirth.Year;

            var validationResult = await ValidateEmployee(employee);
            if (!validationResult.IsValid)
            {
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
            var validationResult = await ValidateEmployee(employee);

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            // Update age
            employee.Age = DateTime.Now.Year - employee.DateOfBirth.Year;
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAsync();
            return validationResult;
        }

        public async Task<byte[]> ExportEmployee()
        {
            var employees = await _employeeRepository.GetAllAsync()
                .Select(e => new Employee
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    DateOfBirth = e.DateOfBirth,
                    Age = e.Age,
                    PhoneNumber = e.PhoneNumber,
                    IdentityId = e.IdentityId,
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
                    }).ToList()
                }).ToListAsync();

            var fileByte = await _exporter.ExportEmployees(employees);
            return fileByte;
        }

        public async Task<Employee> CreateEmployeeFromExcel(IXLRow row)
        {
            var name = row.Cell(1).GetValue<string>()?.Trim();

            var dateOfBirthString = row.Cell(2).GetValue<string>();
            var dateOfBirth = DateOnly.FromDateTime(DateTime.Parse(dateOfBirthString));

            var age = row.Cell(3).GetValue<int>();
            var phoneNumber = row.Cell(4).GetValue<string>()?.Trim();
            var identityNumber = row.Cell(5).GetValue<string>()?.Trim();

            var jobName = row.Cell(6).GetValue<string>()?.Trim();
            var jobId = (await _jobRepository.GetAsync(j => j.Title == jobName))?.JobId;

            var ethicName = row.Cell(7).GetValue<string>()?.Trim();
            var ethicId = (await _ethicRepository.GetAsync(e => e.Name == ethicName))?.EthicId;

            var wardName = row.Cell(8).GetValue<string>()?.Trim();
            var wardId = (await _wardRepository.GetAsync(w => w.Name == wardName))?.WardId;

            var districtName = row.Cell(9).GetValue<string>()?.Trim();
            var districtId = (await _districtRepository.GetAsync(d => d.Name == districtName))?.DistrictId;

            var cityName = row.Cell(10).GetValue<string>()?.Trim();
            var cityId = (await _cityRepository.GetAsync(c => c.Name == cityName))?.CityId;
            var address = row.Cell(11).GetValue<string>()?.Trim();

            var employee = new Employee
            {
                Name = name,
                DateOfBirth = dateOfBirth,
                Age = age,
                PhoneNumber = phoneNumber,
                IdentityId = identityNumber,
                JobId = jobId,
                EthicId = ethicId,
                WardId = wardId ?? 0,
                DistrictId = districtId ?? 0,
                CityId = cityId ?? 0,
                Address = address
            };
            return employee;
        }

        public async Task<List<string>> ImportEmployees(IFormFile file)
        {
            var errors = new List<string>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        errors.Add("No worksheet found in the Excel file.");
                        return (errors);
                    }

                    var rows = worksheet.RowsUsed().Skip(1); // Skip header row
                    int rowIndex = 1;
                    foreach (var row in rows)
                    {
                        // Check if all fields are null or empty
                        if (row.Cells().All(cell => cell.IsEmpty()))
                        {
                            continue;
                        }

                        var employee = await CreateEmployeeFromExcel(row);

                        var validationResult = await ValidateEmployee(employee);
                        if (!validationResult.IsValid)
                        {
                            errors.Add($"Row {rowIndex}: {validationResult.ToString()}");
                        }
                        else
                        {
                            await _employeeRepository.AddAsync(employee);
                            await _employeeRepository.SaveAsync();
                        }
                        rowIndex++;
                    }
                }
            }
            return errors;
        }
    }
}

