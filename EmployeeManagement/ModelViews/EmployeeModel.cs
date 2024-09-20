namespace EmployeeManagement.ModelViews
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IdentityId { get; set; }

        public int? JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public int? EthicId { get; set; }
        public string EthicName { get; set; } = string.Empty;
        public int WardId { get; set; }
        public string WardName { get; set; } = string.Empty;
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = string.Empty;
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public virtual List<DiplomaModel> Diplomas { get; set; } = new List<DiplomaModel>();
    }
}
