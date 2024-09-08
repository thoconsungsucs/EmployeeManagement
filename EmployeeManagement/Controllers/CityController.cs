using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        public async Task<ActionResult> Index()
        {
            var cityList = await _cityService.GetAllAsync();
            return View(cityList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(City city)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View(city);
            }
            var result = await _cityService.AddAsync(city);
            if (!result.IsValid)
            {
                // Model is invalid, add errors to ModelState
                result.AddToModelState(this.ModelState);
                return View(nameof(Create), city);
            }
            TempData["Success"] = "City added successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var city = await _cityService.GetByIdAsync(id);
            return View(city);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(City city)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View(city);
            }
            var result = await _cityService.UpdateAsync(city);
            if (!result.IsValid)
            {
                // Model is invalid, add errors to ModelState
                result.AddToModelState(this.ModelState);
                return View(nameof(Edit), city);
            }
            await _cityService.UpdateAsync(city);
            TempData["Success"] = "City updated successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var city = await _cityService.GetByIdAsync(id);

            return View(city);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(City city)
        {
            await _cityService.DeleteAsync(city);
            TempData["Success"] = "City deleted successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _cityService.GetAllAsync() });
        }
        #endregion
    }
}
