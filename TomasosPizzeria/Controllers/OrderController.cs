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

        private IRestaurantRepository _repository;
        private Kundvagn kundvagn;

        public OrderController(IRestaurantRepository repo, Kundvagn kundvagnService)
        {
            _repository = repo;
            kundvagn = kundvagnService;
        }

        [Authorize]
        public IActionResult PlaceOrder()
        {
            _repository.SaveOrder(User.Identity.Name, User.IsInRole("PremiumUser"));

            return RedirectToAction("ShowMenu", "Menu");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowOrders()
        {
            var orders = _repository.GetAllOrders()
                .Include(o => o.Kund);

            return View(orders);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeOrderStatus(bool status, int orderid)
        {
            
            var result = _repository.ChangeOrderStatus(orderid,status);

         
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
            _repository.DeleteOrder(id);
            return RedirectToAction("ShowOrders");
        }

    }
}