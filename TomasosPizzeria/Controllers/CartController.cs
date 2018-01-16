using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace TomasosPizzeria.Controllers
{
    public class CartController : Controller
    {
        private IMatrattRepository repository;

        public CartController(IMatrattRepository repo) { repository = repo; }

        [HttpPost]
        public RedirectToActionResult AddToCart(int mattrattId)
        {
            Matratt valdmatratt = repository.GetMatratter().FirstOrDefault(p => p.MatrattId == mattrattId);

            if (valdmatratt != null)
            {
                Kundvagn vagn = GetCart(); // Hämtar kundvagn om den finns, annars skapas ny
                vagn.AddItem(valdmatratt); // Lägg till maträtten till kundvagnen
                SaveCart(vagn); // Spara kundvagnen i sessionsvariabel
            }
            return RedirectToAction("ShowMenu","Menu");
        }

        public RedirectToActionResult RemoveFromCart(int matrattId)
        {
            Matratt valdmatratt = repository.GetMatratter().FirstOrDefault(p => p.MatrattId == matrattId);
            if (valdmatratt != null)
            {
                Kundvagn cart = GetCart();
                cart.RemoveLine(valdmatratt);
                SaveCart(cart); 
            }
            return RedirectToAction("ShowMenu", "Menu");
        }

        private Kundvagn GetCart()
        {
            if (HttpContext.Session.GetString("MinKundvagn") != null)
            {
                var str = HttpContext.Session.GetString("MinKundVagn");
                Kundvagn kundvagn = JsonConvert.DeserializeObject<Kundvagn>(str);
                return kundvagn;
            }
            else return new Kundvagn();
        }

        private void SaveCart(Kundvagn vagn)
        {       
            var serializedValue = JsonConvert.SerializeObject(vagn);
            HttpContext.Session.SetString("MinKundVagn", serializedValue);
        }

        
    }
}