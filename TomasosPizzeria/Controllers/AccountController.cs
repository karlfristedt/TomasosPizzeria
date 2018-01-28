using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TomasosPizzeria.Models;
using TomasosPizzeria.Models.ViewModels;
using TomasosPizzeria.Entities;
using TomasosPizzeria.Repositories;

namespace TomasosPizzeria.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private IRestaurantRepository _restaurantrepo;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IRestaurantRepository repo, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _restaurantrepo = repo;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() { return View(); }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var logincandidate = await _userManager.FindByNameAsync(login.UserName);

                if (logincandidate != null)
                {
                    await _signInManager.SignOutAsync();

                    var loginresult =
                        await _signInManager.PasswordSignInAsync(logincandidate, login.Password,true,false);

                    if (loginresult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Fel användarnamn eller lösenord");
                }
            }
            
            return View(login);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newidentityuser = new ApplicationUser
                {
                    UserName = model.UserName,
                };

                var newuser = new Kund // För att lägga till i huvuddatabasen
                {
                    Namn = model.FullName,
                    Email = model.Email,
                    AnvandarNamn = model.UserName,
                    Postnr = model.PostalCode,
                    Gatuadress = model.Adress,
                    Postort = model.City,
                    Telefon = model.Phone,
                    Losenord = model.Password
                };

                _restaurantrepo.AddCustomer(newuser);

                var identityresult = await _userManager.CreateAsync(newidentityuser, model.Password);

                if (identityresult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newidentityuser, "RegularUser");
                    return View("RegistrationSuccess");
                }
                else
                {
                    foreach (IdentityError error in identityresult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        
       
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShowUsers()
        {
            
            var regularusers = await _userManager.GetUsersInRoleAsync("RegularUser");

            var premiumusers = await _userManager.GetUsersInRoleAsync("PremiumUser");

            var temp = regularusers.Select(s => new UsersViewModel
            {
                UserName = s.UserName,
                Role = "RegularUser"
            });

            var test = premiumusers.Select(s => new UsersViewModel
            {
                UserName = s.UserName,
                Role = "PremiumUser"
            });

            var usertest = temp.Concat(test);

            ViewBag.Roles = _roleManager.Roles.ToList().Select(item =>
                new SelectListItem { Text = item.Name.ToString(), Value = item.Name });

            return View(usertest);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var identityuser = await _userManager.FindByNameAsync(username);
            _restaurantrepo.DeleteCustomer(username);
            
            await _userManager.DeleteAsync(identityuser);
            return RedirectToAction("ShowUsers");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(string role, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var usercurrentroles = await _userManager.GetRolesAsync(user);
            var userrole = usercurrentroles[0];
            if (userrole != role)
            {
                await _userManager.AddToRoleAsync(user, role);
                await _userManager.RemoveFromRoleAsync(user, userrole);
                ViewBag.Role = role;
            }
            else ViewBag.Role = userrole;
            return PartialView("_RolePartialView");
        }

    }
}