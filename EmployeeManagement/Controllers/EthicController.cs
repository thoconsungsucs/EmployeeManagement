using EmployeeManagement.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EthicController : Controller
    {
        private readonly IEthicService _ethicService;
        public EthicController(IEthicService ethicService)
        {
            _ethicService = ethicService;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ethics = await _ethicService.GetAllAsync();
            return Json(new { data = ethics });
        }
        #endregion
    }
}
