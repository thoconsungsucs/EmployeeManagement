using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagement.Controllers
{
    public class DistrictController : Controller
    {
        private readonly IDistrictService _districtService;
        private readonly ICityService _cityService;
        public DistrictController(IDistrictService districtService, ICityService cityService)
        {
            _districtService = districtService;
            _cityService = cityService;
        }
        public async Task<IActionResult> Index()
        {
            var districts = await _districtService.GetAllAsync();
            return View(districts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cities = await _cityService.GetAllAsync();
            var districtVM = new DistrictVM
            {
                District = new District(),
                Cities = cities.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CityId.ToString()
                })
            };
            return View(districtVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DistrictVM districtVM)
        {
            var cities = await _cityService.GetAllAsync();


            // Validate the model
            if (!ModelState.IsValid)
            {
                // Get City list
                districtVM.Cities = cities.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CityId.ToString()
                });
                return View(districtVM);
            }
            var result = await _districtService.AddAsync(districtVM.District);
            if (!result.IsValid)
            {
                // Get City list
                districtVM.Cities = cities.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CityId.ToString()
                });

                // Model is invalid, add errors to ModelState
                result.AddToModelState(this.ModelState);
                return View(nameof(Create), districtVM);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var districtVM = new DistrictVM
            {
                District = await _districtService.GetByIdAsync(id),
                Cities = (await _cityService.GetAllAsync()).Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CityId.ToString()
                })
            };
            return View(districtVM);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(DistrictVM districtVM)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                // Get City list
                districtVM.Cities = (await _cityService.GetAllAsync()).Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CityId.ToString()
                });
                return View(districtVM);
            }
            var result = await _districtService.UpdateAsync(districtVM.District);
            if (!result.IsValid)
            {
                // Model is invalid, add errors to ModelState
                result.AddToModelState(this.ModelState);
                // Get City list
                districtVM.Cities = (await _cityService.GetAllAsync()).Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CityId.ToString()
                });
                return View(nameof(Edit), districtVM);
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


        #region
        [HttpGet]
        public async Task<IActionResult> GetDistrictsByCityId(int cityId)
        {
            var districts = await _districtService.GetAllAsync(cityId);
            return Json(new { data = districts });
        }
        #endregion
    }

}
