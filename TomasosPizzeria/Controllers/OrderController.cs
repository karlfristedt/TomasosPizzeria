using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Models;
using TomasosPizzeria.Repositories;

namespace TomasosPizzeria.Controllers
{
    public class OrderController : Controller
    {
        private IRestaurantRepository repository;
        private Kundvagn kundvagn;

        public OrderController(IRestaurantRepository repo, Kundvagn kundvagnService)
        {
            repository = repo;
            kundvagn = kundvagnService;
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            repository.SaveOrder();

            return RedirectToAction("ShowMenu", "Menu");
        }

      
    }
}