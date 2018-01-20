using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Models;
using Newtonsoft.Json;
using TomasosPizzeria.Entities;
using TomasosPizzeria.Repositories;


namespace TomasosPizzeria.Controllers
{
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