using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using TomasosPizzeria.Repositories;
using TomasosPizzeria.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Entities;
using TomasosPizzeria.Service;

namespace TomasosPizzeria.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private IRestaurantRepository _repository;
        private IRestaurantViewService _restaurantViewService;

        public MenuController(IRestaurantRepository repository, IRestaurantViewService restaurantViewService)
        {
            _repository = repository;
            _restaurantViewService = restaurantViewService;
        }

        public IActionResult ShowMenu()
        {
            var kategorier = _repository.GetAllMatrattTyp()
                .Include(m => m.Matratt)
                    .ThenInclude(m => m.MatrattProdukt)
                        .ThenInclude(m => m.Produkt)
                .ToList();

            return View(kategorier);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowEditMenu()
        {
            var kategorier = _repository.GetAllMatrattTyp()
                .Include(m => m.Matratt)
                .ThenInclude(m => m.MatrattProdukt)
                .ThenInclude(m => m.Produkt)
                .ToList();

            return View(kategorier);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EditDish(int matrattId)
        {
            return View(_restaurantViewService.GetMatratt(matrattId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditDish(EditDishViewModel model)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateMatratt(model);
                _repository.UpdateMatrattProdukter(model);

                return RedirectToAction("ShowEditMenu");
            }

            return View(model);

        }
    }
}