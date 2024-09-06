using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Diploma
    {
        public int DiplomaId { get; set; }
        [Required]
        public string? Name { get; set; }

        public DateOnly IssuedDate { get; set; }

        [Required]
        public string? IssuedBy { get; set; }

        public DateOnly ExpiryDate { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
