using Compnay.BLL.Interfaces;
using Compnay.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    // MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IDepartmentReopsitory _departmentReopsitory;

        // ASK CLR Create Object From DepartmentReopsitory

        public DepartmentController(IDepartmentReopsitory departmentReopsitory)
        {
            _departmentReopsitory = departmentReopsitory;
        }

        [HttpGet] // GET : /Department/Index
        public IActionResult Index()
        {

            var deaprment = _departmentReopsitory.GetAll();

            return View(deaprment);
        }
    }
}
