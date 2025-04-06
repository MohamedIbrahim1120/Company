using Company.DAL.Models;
using Company.PL.Dtos;
using Company.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Company.PL.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }



        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> roles;

            if (string.IsNullOrEmpty(SearchInput))
            {
                roles =  _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                   Id = U.Id,
                   Name = U.Name
                });
            }
            else
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name
                }).Where(R => R.Name.ToLower().Contains(SearchInput.ToLower()));
            }

            #region Dictionary
            // Dictionary : 
            // 1. ViewData : Transfer Extra Inforamtion From Controller (Action) To View 

            //ViewData["Message"] = "Hello From ViewData";


            // 2. ViewBag  : Transfer Extra Inforamtion From Controller (Action) To View 
            //ViewBag.Message = new { Message = "Hello From ViewBag" };


            // 3. TempData : 
            #endregion


            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
               
              var role = await _roleManager.FindByNameAsync(model.Name);
                if(role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name,
                    };

                   var result = await _roleManager.CreateAsync(role);
                    if(result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id == null) return NotFound();

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return NotFound();

            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(viewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, nameof(Edit));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto dto)
        {
            if (ModelState.IsValid)
            {
                if (id != dto.Id) return BadRequest();
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null) return NotFound();
                 
               var roleResult = await _roleManager.FindByNameAsync(dto.Name);
                if (roleResult is not null)
                {
                    role.Name = dto.Name;

                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                ModelState.AddModelError("", "Invalid Operations !");
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, nameof(Delete));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturnDto dto)
        {
            if (ModelState.IsValid)
            {
                if (id != dto.Id) return BadRequest();
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null) return NotFound();

                
                    role.Name = dto.Name;

                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                
                ModelState.AddModelError("", "Invalid Operations !");
            }
            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
           var role = await _roleManager.FindByIdAsync(roleId);
            if(role is null) return NotFound();

            ViewData["RoleId"] = roleId;

            var usersInRole = new List<UsersInRoleViewDto>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInRole = new UsersInRoleViewDto()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true; 
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId,List<UsersInRoleViewDto> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if(role is null) return NotFound();

            if (ModelState.IsValid)
            {

                foreach (var user in users)
                {
                    var appuser = await _userManager.FindByIdAsync(user.UserId);
                    if (appuser is not null)
                    {
                        if (user.IsSelected && ! await _userManager.IsInRoleAsync(appuser,role.Name))
                        {
                           await _userManager.AddToRoleAsync(appuser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appuser, role.Name))
                        {
                           await _userManager.RemoveFromRoleAsync(appuser, role.Name);
                        }
                    }
                }
                return RedirectToAction(nameof(Edit),new {id = roleId});
            }
            return View(users);
        }
    }
}
