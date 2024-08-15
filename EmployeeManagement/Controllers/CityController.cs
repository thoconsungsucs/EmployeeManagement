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
            if (!ModelState.IsValid)
            {
                return View(city);
            }
            await _cityService.AddAsync(city);
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
            if (!ModelState.IsValid)
            {
                return View(city);
            }
            await _cityService.UpdateAsync(city);
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
            return RedirectToAction("Index");
        }
    }
}
