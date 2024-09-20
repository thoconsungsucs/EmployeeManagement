using EmployeeManagement.ModelViews;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagement.Models.ViewModels
{
    public class WardVM
    {
        public WardModel WardModel { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Districts { get; set; }
    }
}
