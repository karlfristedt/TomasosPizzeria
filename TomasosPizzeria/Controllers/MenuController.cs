using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using TomasosPizzeria.Repositories;
using TomasosPizzeria.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Entities;

namespace TomasosPizzeria.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private IRestaurantRepository repository;

        public MenuController(IRestaurantRepository repo)
        {
            repository = repo;
        }

        public IActionResult ShowMenu()
        {
            var kategorier = repository.GetAllMatrattTyp()
                .Include(m => m.Matratt)
                    .ThenInclude(m => m.MatrattProdukt)
                        .ThenInclude(m => m.Produkt)
                .ToList();

            return View(kategorier);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowEditMenu()
        {
            var kategorier = repository.GetAllMatrattTyp()
                .Include(m => m.Matratt)
                .ThenInclude(m => m.MatrattProdukt)
                .ThenInclude(m => m.Produkt)
                .ToList();

            return View(kategorier);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EditDish(int id)
        {
            var matratt = repository.GetMatrattById(id);
            
            var produkter = matratt.MatrattProdukt.Where(x => x.MatrattId == id).Select(y => y.Produkt);

            var model = new EditDishViewModel
            {
                MatrattNamn = matratt.MatrattNamn,
                MatrattTyp = matratt.MatrattTypNavigation.Beskrivning,
                Pris = matratt.Pris,
                Beskrivning = matratt.Beskrivning,
            };

           model.Produkter = produkter;

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditDish(EditDishViewModel model)
        {
            //var matratt = repository.GetMatrattById(id);

            //var produkter = matratt.MatrattProdukt.Where(x => x.MatrattId == id).Select(y => y.Produkt);

            //var model = new EditDishViewModel
            //{
            //    MatrattNamn = matratt.MatrattNamn,
            //    MatrattTyp = matratt.MatrattTypNavigation.Beskrivning,
            //    Pris = matratt.Pris,
            //};

            //model.Produkter = produkter;

            return RedirectToAction("ShowEditMenu");
        }
    }
}