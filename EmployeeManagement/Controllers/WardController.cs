using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.ModelViews;
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
            var wards = await _wardService.GetAllFilterAsync();
            return View(wards);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WardModel wardModel)
        {

            var result = await _wardService.AddAsync(wardModel);
            if (!result.IsValid)
            {
                // Model is invalid, add errors to ModelState
                result.AddToModelState(this.ModelState);
                return View(nameof(Create), wardModel);
            }
            TempData["Success"] = "Ward added successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var wardVM = new WardVM
            {
                WardModel = await _wardService.GetByIdAsync(id),
                Districts = (await _districtService.GetAllAsync()).Select(c => new SelectListItem
                {
                    Text = c.DistrictName,
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
                    Text = d.DistrictName,
                    Value = d.DistrictId.ToString()
                });
                return View(wardVM);
            }
            var result = await _wardService.UpdateAsync(wardVM.WardModel);
            if (!result.IsValid)
            {
                // Model is invalid, add errors to ModelState
                result.AddToModelState(this.ModelState);
                // Get District list
                wardVM.Districts = (await _districtService.GetAllAsync()).Select(d => new SelectListItem
                {
                    Text = d.DistrictName,
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
        public async Task<IActionResult> Delete(WardModel wardModel)
        {
            await _wardService.DeleteAsync(wardModel);
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
