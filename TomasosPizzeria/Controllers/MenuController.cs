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
    }
}