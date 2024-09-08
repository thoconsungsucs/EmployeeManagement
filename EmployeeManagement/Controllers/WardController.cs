using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagement.Controllers
{
    public class WardController : Controller
    {
        private readonly IDistrictService _districtService;
        private readonly IWardService _wardService;
        public WardController(IDistrictService districtService, IWardService wardService)
        {
            _districtService = districtService;
            _wardService = wardService;
        }
        public async Task<IActionResult> Index()
        {
            var wards = await _wardService.GetAllAsync();
            return View(wards);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ward ward)
        {

            var result = await _wardService.AddAsync(ward);
            if (!result.IsValid)
            {
                // Model is invalid, add errors to ModelState
                result.AddToModelState(this.ModelState);
                return View(nameof(Create), ward);
            }
            TempData["Success"] = "Ward added successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var wardVM = new WardVM
            {
                Ward = await _wardService.GetByIdAsync(id),
                Districts = (await _districtService.GetAllAsync()).Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.DistrictId.ToString()
                })
            };
            return View(wardVM);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(WardVM wardVM)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                // Get District list
                wardVM.Districts = (await _districtService.GetAllAsync()).Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.DistrictId.ToString()
                });
                return View(wardVM);
            }
            var result = await _wardService.UpdateAsync(wardVM.Ward);
            if (!result.IsValid)
            {
                // Model is invalid, add errors to ModelState
                result.AddToModelState(this.ModelState);
                // Get District list
                wardVM.Districts = (await _districtService.GetAllAsync()).Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.DistrictId.ToString()
                });
                return View(nameof(Edit), wardVM);
            }
            TempData["Success"] = "Ward updated successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ward = await _wardService.GetByIdAsync(id);
            return View(ward);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Ward ward)
        {
            await _wardService.DeleteAsync(ward);
            TempData["Success"] = "Ward deleted successfully";
            return RedirectToAction("Index");
        }

        #region
        [HttpGet]
        public async Task<IActionResult> GetWardsByDistrictId(int districtId)
        {
            var wards = await _wardService.GetAllAsync(districtId);
            return Json(new { data = wards });
        }
        #endregion
    }
}
