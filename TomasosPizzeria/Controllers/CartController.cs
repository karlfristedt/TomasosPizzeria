using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using TomasosPizzeria.Entities;
using TomasosPizzeria.Models;
using TomasosPizzeria.Repositories;


namespace TomasosPizzeria.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IRestaurantRepository repository;
        private Kundvagn kundvagn;

        public CartController(IRestaurantRepository repo, Kundvagn kundvagnService)
        {
            repository = repo;
            kundvagn = kundvagnService;
        }

        [HttpPost]
        public RedirectToActionResult AddToCart(int id)
        {
            Matratt valdmatratt = repository.GetAllMatratter().FirstOrDefault(p => p.MatrattId == id);
            
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
            return View(aktuellvagn);
        }

        
    }
}