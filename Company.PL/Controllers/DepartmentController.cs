using Company.DAL.Models;
using Company.PL.Dtos;
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

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDtoDepartment model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                };
              var count =  _departmentReopsitory.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
