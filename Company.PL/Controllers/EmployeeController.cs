using AutoMapper;
using Company.DAL.Models;
using Company.PL.Dtos;
using Company.PL.Helpers;
using Company.PL.Mapping;
using Compnay.BLL.Interfaces;
using Compnay.BLL.Repositories;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

   
        private readonly IMapper _mapper;

        public EmployeeController(
            IUnitOfWork unitOfWork

            ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchEmployee)
        {
            IEnumerable<Employee> employee;

            if (string.IsNullOrEmpty(SearchEmployee))
            {
                 employee = await _unitOfWork.EmployeeReopsitory.GetAllAsync();
                    
            }
            else
            {
                 employee = await _unitOfWork.EmployeeReopsitory.GetByNameAsync(SearchEmployee);

            }

            #region Dictionary
            // Dictionary : 
            // 1. ViewData : Transfer Extra Inforamtion From Controller (Action) To View 

            //ViewData["Message"] = "Hello From ViewData";


            // 2. ViewBag  : Transfer Extra Inforamtion From Controller (Action) To View 
            //ViewBag.Message = new { Message = "Hello From ViewBag" };


            // 3. TempData : 
            #endregion


            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var dep = await _unitOfWork.DepartmentReopsitory.GetAllAsync();
            ViewData["dep"] = dep;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDtoEmployee dto)
        {
            if (ModelState.IsValid)
            {
                #region Manual Mapping
                // Manual Mapping
                //var Emp = new Employee()
                //{
                //    Name = dto.Name,
                //    Age = dto.Age,
                //    Email = dto.Email,
                //    Address = dto.Address,
                //    Phone = dto.Phone,
                //    Salary = dto.Salary,
                //    IsActive = dto.IsActive,
                //    IsDeleted = dto.IsDeleted,
                //    HiringDate = dto.HiringDate,
                //    CreateAt = dto.CreateAt,
                //    DepartmentId = dto.DepartmentId,
                //};
                #endregion

                if(dto.Image is not null)
                {
                  dto.ImageName = DocumentSettings.UploadFile(dto.Image, "images");
                }
                var Emp =   _mapper.Map<Employee>(dto);
                 await _unitOfWork.EmployeeReopsitory.AddAsync(Emp);

                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    TempData["Message"] = "Employee Is Created";
                   return RedirectToAction(nameof(Index));
                }
            }
            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) return NotFound();

            var emp = await _unitOfWork.EmployeeReopsitory.GetAsync(id.Value);
            
            if (emp == null) return NotFound();

            var dto = _mapper.Map<CreateDtoEmployee>(emp);

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, string viewName = "Edit")
        {
            if(id is null) return BadRequest();
            var emp = await _unitOfWork.EmployeeReopsitory.GetAsync(id.Value);

            var dep = await _unitOfWork.DepartmentReopsitory.GetAllAsync();

            ViewData["dep"] = dep;

            if (emp is null) return NotFound();

            var dto = _mapper.Map<CreateDtoEmployee>(emp);
            return View(viewName,dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateDtoEmployee dto, string viewName = "Edit")
        {
            if(ModelState.IsValid)
            {
                if(dto.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(dto.ImageName, "images");
                }
                if(dto.Image is not null)
                {
                    dto.ImageName = DocumentSettings.UploadFile(dto.Image, "images");
                }
                var Emp = _mapper.Map<Employee>(dto);
                Emp.Id = id;
                _unitOfWork.EmployeeReopsitory.Update(Emp);

                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    TempData["Message"] = "Employee Is Updated";
                    return RedirectToAction(nameof(Index));
                }
            }
         
            return View(viewName,dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var emp = await _unitOfWork.EmployeeReopsitory.GetAsync(id.Value);

            if (emp == null) return NotFound();

            var dto = _mapper.Map<CreateDtoEmployee>(emp);

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id,Employee dto)
        {
            _unitOfWork.EmployeeReopsitory.Delete(dto);
            var count = await _unitOfWork.CompleteAsync();
            if (count > 0)
            {
                TempData["Message"] = "Employee Is Created";
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }
    }
}
