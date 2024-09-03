using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace EmployeeManagement.Models.ViewModels
{
    public class DistrictVM
    {
        public District District { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}
