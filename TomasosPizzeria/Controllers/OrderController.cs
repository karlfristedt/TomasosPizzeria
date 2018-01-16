using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.Controllers
{
    public class OrderController : Controller
    {
        private IMatrattRepository repository;

        public OrderController(IMatrattRepository repo) { repository = repo; }

        public IActionResult OrderDish(int id)
        {
            //var selecteddish = repository.GetMatratter().SingleOrDefault(m => m.MatrattId == id);
            //var customer = repository.GetCustomers().SingleOrDefault(c => c.KundId);

            //var neworder = new Bestallning();
            //neworder.BestallningDatum = DateTime.Now;
            //neworder.KundId = 1;
            //neworder.Levererad = false;
            //neworder.Totalbelopp += selecteddish.Pris;

            //repository.SaveOrder(neworder);



            //var newbestallmatratt = new BestallningMatratt();
            //newbestallmatratt.
            //neworder.BestallningMatratt.Add




            return RedirectToAction("ShowMenu", "Menu");
        }

        public IActionResult AddToCart(int id)
        {
            //if (HttpContext.Session.Keys.Any())
            //{
            //    var str = HttpContext.Session.GetString("MyProducts");
            //    List<Produkt> prodlist = JsonConvert.DeserializeObject<List<Produkt>>(str);
            //    if (prodlist.Exists(x => x.Namn == product) == false)
            //    {
            //        prodlist.Add(new Produkt { Antal = quantity, Namn = product, Pris = price });
            //        var serializedValue1 = JsonConvert.SerializeObject(prodlist);

            //        HttpContext.Session.SetString("MyProducts", serializedValue1);
            //        return View("SelectProducts", GetListItems());
            //    }
            //    else return View("Message2");
            //}

            //var produktlista = new List<Produkt>();

            //var newprodukt = new Produkt() { Namn = product, Antal = quantity, Pris = price };

            //produktlista.Add(newprodukt);

            //var serializedValue = JsonConvert.SerializeObject(produktlista);

            //HttpContext.Session.SetString("MyProducts", serializedValue);
            return RedirectToAction("ShowMenu", "Menu");
        }
    }
}