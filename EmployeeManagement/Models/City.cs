namespace EmployeeManagement.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public List<District> Districts { get; set; } = new List<District>();
    }
}
