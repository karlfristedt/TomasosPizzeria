using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        private IRestaurantRepository repository;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IRestaurantRepository repo)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            repository = repo;
        }

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
        [Authorize(Roles="Admin")]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newidentityuser = new ApplicationUser
                {
                    Name = model.FullName,
                    Email = model.Email,
                    UserName = model.UserName,
                    PostalCode = model.PostalCode,
                    Adress = model.Adress,
                    City = model.City,
                    PhoneNumber = model.Phone,
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

                repository.AddCustomer(newuser);

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
        public async Task<IActionResult> EditUser(string id)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShowUsers()
        {
            var regularusers = await _userManager.GetUsersInRoleAsync("RegularUser");
            var premiumusers = await _userManager.GetUsersInRoleAsync("PremiumUser");
            

            var users = new UsersViewModel
            {
                RegularUsers = regularusers,
                PremiumUsers = premiumusers
            };


            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            await _userManager.DeleteAsync(user);
            return RedirectToAction("ShowUsers");
        }


    }
}