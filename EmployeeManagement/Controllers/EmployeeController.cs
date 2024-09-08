using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Models;
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
            var employeeList = await _employeeService.GetAllAsync(filter);
            return View(employeeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            var result = await _employeeService.AddAsync(employee);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(employee);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            var result = await _employeeService.UpdateAsync(employee);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(employee);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Employee employee)
        {
            await _employeeService.DeleteAsync(employee);
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
