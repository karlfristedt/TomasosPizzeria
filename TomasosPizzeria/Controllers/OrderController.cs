using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Entities;
using TomasosPizzeria.Models;
using TomasosPizzeria.Repositories;

namespace TomasosPizzeria.Controllers
{
    public class OrderController : Controller
    {
        private IMatrattRepository repository;
        private Kundvagn kundvagn;

        public OrderController(IMatrattRepository repo, Kundvagn kundvagnService)
        {
            repository = repo;
            kundvagn = kundvagnService;
        }

        public IActionResult PlaceOrder()
        {
            Kundvagn vagn = kundvagn;

            Bestallning nybest = new Bestallning();
            nybest.BestallningDatum = DateTime.Now;
            //nybest.Kund = repository.GetCustomersById(1); // Måste ändras senare
            //nybest.BestallningId = 1; Behövs detta?
            repository.SaveOrder(nybest);

            BestallningMatratt nyBestallningMatratt = new BestallningMatratt();
            nyBestallningMatratt.Bestallning = nybest;
            

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

      
    }
}