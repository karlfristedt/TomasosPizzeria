using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Entities;
using TomasosPizzeria.Models;
using TomasosPizzeria.Repositories;


namespace TomasosPizzeria.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IRestaurantRepository _repository;
        private Kundvagn kundvagn;
        

        public CartController(IRestaurantRepository repo, Kundvagn kundvagnService)
        {
            _repository = repo;
            kundvagn = kundvagnService;
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToActionResult AddToCart(int id)
        {
           Matratt valdmatratt = _repository.GetAllMatratter().FirstOrDefault(p => p.MatrattId == id);
           

            if (valdmatratt != null)
            {
                Kundvagn vagn = kundvagn; // Hämtar kundvagn om den finns, annars skapas ny
                vagn.AddItem(valdmatratt); // Lägg till maträtten till kundvagnen
            }
            return RedirectToAction("ShowMenu","Menu");
        }
      
        public IActionResult ShowCart()
        {
            Kundvagn aktuellvagn = kundvagn;
            if (aktuellvagn.GetOrderrader().Count() == 0)
            {
                return View("EmptyCartMessage");
            }
            
            ViewBag.Antalratter = kundvagn.GetAntalRatter();
            ViewBag.Antalpoang = _repository.GetPoangByUserName(User.Identity.Name) + kundvagn.GetAntalRatter()*10;

            return View(aktuellvagn);
        }

    }
}