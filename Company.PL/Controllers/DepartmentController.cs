using Company.DAL.Models;
using Company.PL.Dtos;
using Compnay.BLL.Interfaces;
using Compnay.BLL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;

namespace Company.PL.Controllers
{
    // MVC Controller
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

      

        public DepartmentController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet] // GET : /Department/Index
        public async Task<IActionResult> Index()
        {

            var deaprment = await _unitOfWork.DepartmentReopsitory.GetAllAsync();

            #region Dictionary
            // Dictionary : 
            // 1. ViewData : Transfer Extra Inforamtion From Controller (Action) To View 

            //ViewData["Message"] = "Hello From ViewData";


            // 2. ViewBag  : Transfer Extra Inforamtion From Controller (Action) To View 

            #endregion

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
        public async Task<IActionResult> Create(CreateDtoDepartment model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                };
             await _unitOfWork.DepartmentReopsitory.AddAsync(department);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id,string viewName = "Details")
        {
            if(id is null) return NotFound();

            var dept = await _unitOfWork.DepartmentReopsitory.GetAsync(id.Value);

            if (dept is null) return NotFound();

            return View(viewName,dept);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest("Invaild Id");

            var dept = await _unitOfWork.DepartmentReopsitory.GetAsync(id.Value);

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
        public async Task<IActionResult> Edit([FromRoute] int id, CreateDtoDepartment department)
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
                     _unitOfWork.DepartmentReopsitory.Update(depDto);
                    var dept = await _unitOfWork.CompleteAsync();

                    if (dept > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(department);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id is null) return BadRequest("Invaild Id");

            var dept = await _unitOfWork.DepartmentReopsitory.GetAsync(id.Value);

            if (dept is null) return NotFound(new { StatusCode = 404, message = $"Deparment With id  :{id} is not Found" });

            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Department department)
        {
            if (!ModelState.IsValid)
            {   
               
                _unitOfWork.DepartmentReopsitory.Delete(department);
                var dept = await _unitOfWork.CompleteAsync();

                if (dept > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View(department);
        }


    }
}

