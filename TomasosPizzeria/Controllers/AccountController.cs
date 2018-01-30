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
        [ValidateAntiForgeryToken]
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


        [Authorize]
        //[ValidateAntiForgeryToken]
        public IActionResult EditUser()
        {
            //var identityuser = await _userManager.FindByNameAsync(User.Identity.Name);
            var user = _restaurantrepo.GetCustomerByUserName(User.Identity.Name);
            
            var userdetails = new EditUserViewModel
            {
                Adress = user.Gatuadress,
                Phone = user.Telefon,
                City = user.Postort,
                PostalCode = user.Postnr,
                FullName = user.Namn,
                Email = user.Email,
                UserName = user.AnvandarNamn
            };

            return View(userdetails);
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityuser = await _userManager.FindByNameAsync(User.Identity.Name);

                var user = _restaurantrepo.GetCustomerByUserName(User.Identity.Name);

                user.Email = model.Email;
                user.Gatuadress = model.Adress;
                user.Namn = model.FullName;
                user.Postnr = model.PostalCode;
                user.Telefon = model.Phone;
                user.Postort = model.City;
                _restaurantrepo.UpdateCustomer();

                if (model.NewPassword != null && model.CurrentPassword != null)
                {
                    var result = await _userManager.ChangePasswordAsync(identityuser, model.CurrentPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return View("EditSuccess");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else return View("EditSuccess");


            }
            
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
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