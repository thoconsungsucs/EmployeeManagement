using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.ModelViews;
using EmployeeManagement.Ultilities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index(Filter? filter)
        {
            var employeeList = await _employeeService.GetAllFilterAsync(filter);
            return View(employeeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeModel);
            }
            var result = await _employeeService.AddAsync(employeeModel);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(employeeModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeModel);
            }
            var result = await _employeeService.UpdateAsync(employeeModel);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(employeeModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeModel employeeModel)
        {
            await _employeeService.DeleteAsync(employeeModel.EmployeeId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return View(employee);
        }

        public async Task<IActionResult> ExportAllEmployees()
        {

            var fileBytes = await _employeeService.ExportEmployee();

            // Return the file as a downloadable Excel file
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllEmployees.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> ImportEmployees(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "Please select a file.";
                return RedirectToAction("Index");
            }

            var errors = await _employeeService.ImportEmployees(file);

            if (errors.Count > 0)
            {
                TempData["Error"] = String.Join("\n", errors);
                return RedirectToAction("Index");
            }
            TempData["Success"] = "Import successful!";
            return RedirectToAction("Index");
        }

    }
}
