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
            
            var matrattprodukter = matratt.MatrattProdukt.Where(x => x.MatrattId == id).Select(y => y.Produkt).ToList();

            var test = repository.GetAllProducts().Select(v => new ProductViewModel
            {
               ProduktNamn = v.ProduktNamn,
               ProduktId = v.ProduktId
            }).ToList();

            foreach (var item in test)
            {
                item.IsSelected = matrattprodukter.Exists(d => d.ProduktId == item.ProduktId);
            }

            var model = new EditDishViewModel
            {
                MatrattNamn = matratt.MatrattNamn,
                MatrattTyp = matratt.MatrattTypNavigation.Beskrivning,
                Pris = matratt.Pris,
                Beskrivning = matratt.Beskrivning,
                MatrattId = matratt.MatrattId
            };

           model.Produkter = test;

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditDish(EditDishViewModel model)
        {
            if (ModelState.IsValid)
            {
                var matrattProdukts = model.Produkter.Where(prod => prod.IsSelected == true).Select(s => new MatrattProdukt
                {
                    MatrattId = model.MatrattId,
                    ProduktId = s.ProduktId,
                }).AsQueryable().Include(x => x.Matratt).Include(y => y.Produkt);

                repository.UpdateMatrattProdukter(model.MatrattId, matrattProdukts);
                return RedirectToAction("ShowEditMenu");
            }

            return View(model);

        }
    }
}