using EmployeeManagement.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await _jobService.GetAllAsync();
            return Json(new { data = jobs });
        }
        #endregion
    }
}
