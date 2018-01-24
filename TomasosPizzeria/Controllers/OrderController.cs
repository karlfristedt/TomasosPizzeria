using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TomasosPizzeria.Models;
using TomasosPizzeria.Repositories;

namespace TomasosPizzeria.Controllers
{
    //[Authorize(Roles = "RegularUser")]
    public class OrderController : Controller
    {
        private IRestaurantRepository repository;

        public OrderController(IRestaurantRepository repo)
        {
            repository = repo;
        }
        
        [Authorize]
        public IActionResult PlaceOrder()
        {
            repository.SaveOrder(User.Identity.Name);

            return RedirectToAction("ShowMenu", "Menu");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowOrders()
        {
            var orders = repository.GetAllOrders()
                .Include(o => o.Kund);

            return View(orders);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeOrderStatus(bool status, int orderid)
        {
            
            var result = repository.ChangeOrderStatus(orderid,status);

         
            if (result)
            {
                if (status==true)
                    ViewBag.Status = "Ja";
                else ViewBag.Status = "Nej";
            }
            else
            {
                if (status == false)
                    ViewBag.Status = "Ja";
                else ViewBag.Status = "Nej";
            }

            return PartialView("_OrderStatusPartialView");
        }

        public IActionResult DeleteOrder(int id)
        {
            repository.DeleteOrder(id);
            return RedirectToAction("ShowOrders");
        }

    }
}