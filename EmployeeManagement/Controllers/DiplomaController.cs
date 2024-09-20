using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.ModelViews;
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
            return View(new DiplomaModel { EmployeeId = employeeId });
        }

        [HttpPost]
        public async Task<ActionResult> Create(DiplomaModel diplomaModel)
        {
            var result = await _diplomaService.AddAsync(diplomaModel);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(diplomaModel);
            }
            return RedirectToAction("Details", "Employee", new { id = diplomaModel.EmployeeId });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var diploma = await _diplomaService.GetDiplomaById(id);
            return View(diploma);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DiplomaModel diplomaModel)
        {
            var result = await _diplomaService.UpdateAsync(diplomaModel);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(diplomaModel);
            }
            TempData["Success"] = "Diploma updated successfully";
            return RedirectToAction("Details", "Employee", new { id = diplomaModel.EmployeeId });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var diploma = await _diplomaService.GetDiplomaById(id);
            await _diplomaService.DeleteAsync(id);
            TempData["Success"] = "Diploma deleted successfully";
            return RedirectToAction("Details", "Employee", new { id = diploma.EmployeeId });
        }


    }
}
