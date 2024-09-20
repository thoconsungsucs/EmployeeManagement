using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ModelViews
{
    public class DiplomaModel
    {
        public int DiplomaId { get; set; }
        [Required]
        public string? Name { get; set; }

        public DateOnly IssuedDate { get; set; }

        [Required]
        public string? IssuedBy { get; set; }

        public DateOnly ExpiryDate { get; set; }

        public int EmployeeId { get; set; }
        public virtual EmployeeModel? EmployeeModel { get; set; }
    }
}
