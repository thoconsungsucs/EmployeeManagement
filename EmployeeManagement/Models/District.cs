using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class District
    {
        public int DistrictId { get; set; }
        public string Name { get; set; }
        [Required]
        public int CityId { get; set; }

        public City? City { get; set; } = null;
        public List<Ward> Wards { get; set; } = new List<Ward>();
    }
}
