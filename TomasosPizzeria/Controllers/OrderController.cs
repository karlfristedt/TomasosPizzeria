using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Models;
using TomasosPizzeria.Repositories;

namespace TomasosPizzeria.Controllers
{
    //[Authorize(Roles = "RegularUser")]
    public class OrderController : Controller
    {
        private IRestaurantRepository repository;
        private Kundvagn kundvagn;

        public OrderController(IRestaurantRepository repo, Kundvagn kundvagnService)
        {
            repository = repo;
            kundvagn = kundvagnService;
        }
        
        public IActionResult PlaceOrder()
        {
            repository.SaveOrder();

            return RedirectToAction("ShowMenu", "Menu");
        }

      
    }
}