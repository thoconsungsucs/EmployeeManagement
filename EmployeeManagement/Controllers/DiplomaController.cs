using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DiplomaController : Controller
    {
        private readonly IDiplomaService _diplomaService;
        public DiplomaController(IDiplomaService diplomaService)
        {
            _diplomaService = diplomaService;
        }

        [HttpGet]
        public ActionResult Create(int employeeId)
        {
            return View(new Diploma { EmployeeId = employeeId });
        }

        [HttpPost]
        public async Task<ActionResult> Create(Diploma diploma)
        {
            var result = await _diplomaService.AddAsync(diploma);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(diploma);
            }
            return RedirectToAction("Details", "Employee", new { id = diploma.EmployeeId });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var diploma = await _diplomaService.GetDiplomaById(id);
            return View(diploma);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Diploma diploma)
        {
            var result = await _diplomaService.UpdateAsync(diploma);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(diploma);
            }
            TempData["Success"] = "Diploma updated successfully";
            return RedirectToAction("Details", "Employee", new { id = diploma.EmployeeId });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var diploma = await _diplomaService.GetDiplomaById(id);
            await _diplomaService.DeleteAsync(diploma);
            TempData["Success"] = "Diploma deleted successfully";
            return RedirectToAction("Details", "Employee", new { id = diploma.EmployeeId });
        }


    }
}
