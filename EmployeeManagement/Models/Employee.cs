using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int EmoloyeeId { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityId { get; set; }
    }
}
