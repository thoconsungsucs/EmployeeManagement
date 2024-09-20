using EmployeeManagement.ModelViews;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace EmployeeManagement.Models.ViewModels
{
    public class DistrictVM
    {
        public DistrictModel DistrictModel { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}
