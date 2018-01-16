using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Models;
using Newtonsoft.Json;


namespace TomasosPizzeria.Controllers
{
    public class CartController : Controller
    {
        private IMatrattRepository repository;
        private Kundvagn kundvagn;

        public CartController(IMatrattRepository repo, Kundvagn kundvagnService)
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
            return View(aktuellvagn);
        }

        
    }
}