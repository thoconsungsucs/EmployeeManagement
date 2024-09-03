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
        public async Task<IActionResult> Create()
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var district = await _districtService.GetByIdAsync(id);
            return View(district);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(District district)
        {
            await _districtService.DeleteAsync(district);
            return RedirectToAction("Index");
        }
    }
}
