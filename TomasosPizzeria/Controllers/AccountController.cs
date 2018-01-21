using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Models;
using TomasosPizzeria.Models.ViewModels;

namespace TomasosPizzeria.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newuser = new ApplicationUser();
                newuser.Name = model.FullName;
                newuser.Email = model.Email;
                newuser.UserName = model.UserName;
                var identityresult = await _userManager.CreateAsync(newuser, model.Password);

                if (identityresult.Succeeded)
                {
                    return View("RegistrationSuccess");
                }
                else
                {
                    foreach (IdentityError error in identityresult.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
            }
            return View(model);
        }


        //public async Task<IActionResult> EditUser(string id)
        //{
        //    return View();
        //}

        public IActionResult ShowUsers()
        {
            var allusers = _userManager.Users;
            return View(allusers);
        }

        public async Task<IActionResult> RemoveUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("ShowUsers");
        }

    }
}