namespace EmployeeManagement.ModelViews
{
    public class CityModel
    {
        public int CityId { get; set; }
        public string? Name { get; set; }
        public List<DistrictModel> Districts { get; set; } = new List<DistrictModel>();
    }
}
