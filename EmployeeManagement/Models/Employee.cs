using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IdentityId { get; set; }

        public int? JobId { get; set; }
        public Job? Job { get; set; }

        public int? EthicId { get; set; }
        public Ethic? Ethic { get; set; }

        public int WardId { get; set; }
        public Ward? Ward { get; set; }

        public int DistrictId { get; set; }
        public District? District { get; set; }

        public int CityId { get; set; }
        public City? City { get; set; }

        public string Address { get; set; }

        public virtual List<Diploma> Diplomas { get; set; } = new List<Diploma>();


    }
}
