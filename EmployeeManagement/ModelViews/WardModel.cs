namespace EmployeeManagement.ModelViews
{
    public class WardModel
    {
        public int WardId { get; set; }
        public string? WardName { get; set; }
        public int DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public string CityName { get; set; } = string.Empty;
    }
}
