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
            var selecteddish = repository.GetMatratter().SingleOrDefault(m => m.MatrattId == id);
            //var customer = repository.GetCustomers().SingleOrDefault(c => c.KundId);




            return RedirectToAction("ShowMenu", "Menu");
        }
    }
}