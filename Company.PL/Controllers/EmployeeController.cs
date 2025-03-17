using AutoMapper;
using Company.DAL.Models;
using Company.PL.Dtos;
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
        private readonly IEmployeeReopsitory _employeeReopsitory;
        private readonly IDepartmentReopsitory _departmentReopsitory;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeReopsitory employeeReopsitory
            ,IDepartmentReopsitory departmentReopsitory
            ,IMapper mapper)
        {
            _employeeReopsitory = employeeReopsitory;
            _departmentReopsitory = departmentReopsitory;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Index(string? SearchEmployee)
        {
            IEnumerable<Employee> employee;

            if (string.IsNullOrEmpty(SearchEmployee))
            {
                 employee = _employeeReopsitory.GetAll();
                    
            }
            else
            {
                 employee = _employeeReopsitory.GetByName(SearchEmployee);

            }
            // Dictionary : 
            // 1. ViewData : Transfer Extra Inforamtion From Controller (Action) To View 

            //ViewData["Message"] = "Hello From ViewData";


            // 2. ViewBag  : Transfer Extra Inforamtion From Controller (Action) To View 
            //ViewBag.Message = new { Message = "Hello From ViewBag" };


            // 3. TempData : 
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var dep =  _departmentReopsitory.GetAll();
            ViewData["dep"] = dep;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee dto)
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


                var Emp =  _mapper.Map<Employee>(dto);
                var count = _employeeReopsitory.Add(Emp);
                if (count > 0)
                {
                    TempData["Message"] = "Employee Is Created";
                   return RedirectToAction(nameof(Index));
                }
            }
            return View(dto);
        }


        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if(id == null) return NotFound();

            var emp = _employeeReopsitory.Get(id.Value);
            
            if (emp == null) return NotFound();

            return View(viewName,emp);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var dep = _departmentReopsitory.GetAll();
            ViewData["dep"] = dep;
            if (id == null) return NotFound();
            var emp = _employeeReopsitory.Get(id.Value);
            if (emp is null) return NotFound();
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee dto)
        {
            if (ModelState.IsValid)
            {
                if (id != dto.Id) return BadRequest();
                var count = _employeeReopsitory.Update(dto);
                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(dto);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return Details(id,nameof(Delete));
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] int id,Employee employee)
        {
            if (ModelState.IsValid)
            {
                if(id == employee.Id)
                {
                   var emp = _employeeReopsitory.Delete(employee);
                    if (emp > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(employee);
        }
    }
}
