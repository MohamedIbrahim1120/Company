using Company.DAL.Models;
using Company.PL.Dtos;
using Company.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
        }

        #region SignUp

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
           
            if(ModelState.IsValid) // Server Side Validation
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is null)
                    {
                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,
                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(SignIn));
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);

                        }
                    }
                }

                ModelState.AddModelError("", "Invlid SignUp");
            }
               
            return View(model);
        }
        // Pass@word123

        #endregion

        #region SignIn

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                   var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        // Sign In
                       var result = await _signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Invlid Login");
            }
            return View(model);
        }

        #endregion

        #region SignOut

        public new async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(SignIn));
        }


        #endregion

        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    //  Generate Token

                   var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Create URL
                   var url = Url.Action("ResetPassword","Account",new {email = model.Email,token},Request.Scheme);

                    // Create
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = url
                    };
                    // Send Email
                    var flag = EmailSetting.SendEmail(email);
                    if (flag)
                    {

                        // Check Your Inbox

                        return RedirectToAction(nameof(CheckYourInbox));

                    }
                }
            }
            ModelState.AddModelError("", "Invaild Reset Password Url !!");
            return View(nameof(ForgetPassword),model);
        }


        [HttpGet]
        public IActionResult CheckYourInbox()
        {

            return View();
        }

        #endregion

        #region Reset Password

        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                if (email is null || token is null) return BadRequest("Invalid Operation");
               var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                   var result = await _userManager.ResetPasswordAsync(user, token,model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn));
                    }
                }

                ModelState.AddModelError("","Invalid Reset Operation");
            }
            return View();
        }

        #endregion


    }
}
