using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [HttpGet]
        public IActionResult ShowEditProduct()
        {
            var editProductView = _repository.GetAllProducts().Select(p => new ProductViewModel
            {
                ProduktNamn = p.ProduktNamn,
                ProduktId = p.ProduktId
            }).ToList();

            return View(editProductView);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditProduct(int produktId)
        {
            var editProductView = new ProductViewModel();
            
            var temp = _repository.GetProduktById(produktId);

            editProductView.ProduktId = temp.ProduktId;
            editProductView.ProduktNamn = temp.ProduktNamn;

            return View(editProductView);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =_repository.UpdateProduct(model);
                if (result)
                {
                    return RedirectToAction("ShowEditProduct");
                }
                ModelState.AddModelError("", "Det finns redan en annan produkt som heter så!");
                
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _repository.AddProduct(model);
                if (result)
                {
                    return RedirectToAction("ShowEditProduct");
                }
                ModelState.AddModelError("", "Det finns redan en annan produkt som heter så!");
            }
            return View(model);
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddDish()
        {
            
            var temp = _restaurantViewService.GetMatratt();
            return View(temp);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddDish(AddDishViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _repository.AddMatratt(model);
                if (result)
                {
                    return RedirectToAction("ShowEditMenu");
                }
                
                ModelState.AddModelError("", "Det finns redan en maträtt som heter så!");
                
            }
            var productViewList = _repository.GetAllProducts().Select(v => new ProductViewModel
            {
                ProduktNamn = v.ProduktNamn,
                ProduktId = v.ProduktId
            }).ToList();

            model.Produkter = productViewList;

            model.MatrattTyper = _repository.GetAllMatrattTyp().Select(item => new SelectListItem
            {
                Text = item.Beskrivning.ToString(),
                Value = item.Beskrivning
            }).ToList();
            
            return View(model);
        }
    }
}