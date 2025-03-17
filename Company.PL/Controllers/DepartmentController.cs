using Company.DAL.Models;
using Company.PL.Dtos;
using Compnay.BLL.Interfaces;
using Compnay.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;

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

            // Dictionary : 
            // 1. ViewData : Transfer Extra Inforamtion From Controller (Action) To View 

            //ViewData["Message"] = "Hello From ViewData";


            // 2. ViewBag  : Transfer Extra Inforamtion From Controller (Action) To View 
            ViewBag.Message = "Hello From ViewBag";


            // 3. TempData : 

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

        [HttpGet]
        public IActionResult Details(int? id,string viewName = "Details")
        {
            if(id is null) return NotFound();

            var dept = _departmentReopsitory.Get(id.Value);

            if (dept is null) return NotFound();

            return View(viewName,dept);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest("Invaild Id");

            var dept = _departmentReopsitory.Get(id.Value);

            if (dept is null) return NotFound(new { StatusCode = 404, message = $"Deparment With id  :{id} is not Found" });
            var depDto = new CreateDtoDepartment()
            {            
                Code = dept.Code,
                Name = dept.Name,
                CreateAt=dept.CreateAt,
            };

            return View(depDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateDtoDepartment department)
        {
            if (ModelState.IsValid)
            {

                var depDto = new Department()
                {
                    Id = id,
                    Code = department.Code,
                    Name = department.Name,
                    CreateAt = department.CreateAt,
                };
                if (id == depDto.Id)
                {
                    var dept = _departmentReopsitory.Update(depDto);
                    if (dept > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(department);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            #region F
            //if (id is null) return BadRequest("Invaild Id");

            //var dept = _departmentReopsitory.Get(id.Value);

            //if (dept is null) return NotFound(new { StatusCode = 404, message = $"Deparment With id  :{id} is not Found" });
            #endregion

            return Details(id,nameof(Delete));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var dept = _departmentReopsitory.Delete(department);
                    if (dept > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(department);
        }


    }
}

