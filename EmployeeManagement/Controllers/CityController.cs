using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.ModelViews;
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
            var cityList = await _cityService.GetAllFilterAsync();
            return View(cityList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CityModel cityModel)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View(cityModel);
            }
            var result = await _cityService.AddAsync(cityModel);
            if (!result.IsValid)
            {
                // Model is invalid, add errors to ModelState
                result.AddToModelState(this.ModelState);
                return View(nameof(Create), cityModel);
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
        public async Task<ActionResult> Edit(CityModel cityModel)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View(cityModel);
            }
            var result = await _cityService.UpdateAsync(cityModel);
            if (!result.IsValid)
            {
                // Model is invalid, add errors to ModelState
                result.AddToModelState(this.ModelState);
                return View(nameof(Edit), cityModel);
            }
            await _cityService.UpdateAsync(cityModel);
            TempData["Success"] = "City updated successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var cityModel = await _cityService.GetByIdAsync(id);
            if (cityModel == null) return NotFound();

            return View(cityModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(CityModel cityModel)
        {
            var result = await _cityService.DeleteAsync(cityModel.CityId);
            if (!result.IsValid)
            {
                // Model is invalid, add errors to ModelState
                result.AddToModelState(this.ModelState);
                return View(nameof(Delete), cityModel);
            }
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
