using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TomasosPizzeria.Repositories;
using TomasosPizzeria.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Entities;

namespace TomasosPizzeria.Controllers
{
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

            //
            //var ratter = repository.GetAllMatratter()
            //    .Include(j => j.MatrattProdukt)
            //    .ThenInclude(i => i.)

            //var menuview = new MenyViewModel();

            //foreach (var typ in kategorier)
            //{
                
            //}

            //var menuview = select ddd from repository.GetAllMatrattTyp()


            //var query = from a in repository.GetAllMatrattTyp()
            //            select new MenyViewModel
            //            {
            //                Matratter.Add(a.Matratt)
            //            };

            return View(kategorier);
        }
    }
}