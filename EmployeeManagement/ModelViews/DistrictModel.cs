namespace EmployeeManagement.ModelViews
{
    public class DistrictModel
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = string.Empty;
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public List<WardModel> Wards { get; set; } = new List<WardModel>();
    }
}
