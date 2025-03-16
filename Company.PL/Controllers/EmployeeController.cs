using Company.DAL.Models;
using Company.PL.Dtos;
using Compnay.BLL.Interfaces;
using Compnay.BLL.Repositories;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeReopsitory _employeeReopsitory;

        public EmployeeController(IEmployeeReopsitory employeeReopsitory)
        {
            _employeeReopsitory = employeeReopsitory;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var employee = _employeeReopsitory.GetAll();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDtoEmployee dto)
        {
            if (ModelState.IsValid)
            {
                var Emp = new Employee()
                {
                    Name = dto.Name,
                    Age = dto.Age,
                    Email = dto.Email,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    Salary = dto.Salary,
                    IsActive = dto.IsActive,
                    IsDeleted = dto.IsDeleted,
                    HiringDate = dto.HiringDate,
                    CreateAt = dto.CreateAt,
                };
                var count = _employeeReopsitory.Add(Emp);
                if (count > 0)
                {
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
            if (id == null) return NotFound();
            var emp = _employeeReopsitory.Get(id.Value);
            if (emp == null) return NotFound();
            var EmpDto = new CreateDtoEmployee()
            {
                Name = emp.Name,
                Age = emp.Age,
                Email = emp.Email,
                Address = emp.Address,
                Phone = emp.Phone,
                Salary = emp.Salary,
                IsActive = emp.IsActive,
                IsDeleted = emp.IsDeleted,
                HiringDate = emp.HiringDate,
                CreateAt = emp.CreateAt,
            };
            return View(EmpDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateDtoEmployee dto)
        {
            if (ModelState.IsValid)
            {
                var Emp = new Employee()
                {
                    Id = id,
                    Name = dto.Name,
                    Age = dto.Age,
                    Email = dto.Email,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    Salary = dto.Salary,
                    IsActive = dto.IsActive,
                    IsDeleted = dto.IsDeleted,
                    HiringDate = dto.HiringDate,
                    CreateAt = dto.CreateAt,
                };
                if (id == Emp.Id)
                {
                    var dept = _employeeReopsitory.Update(Emp);
                    if (dept > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
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
