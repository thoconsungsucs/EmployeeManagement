using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Ward
    {
        [Key]
        public int WardId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int DistrictId { get; set; }
        public District? District { get; set; }


    }
}
