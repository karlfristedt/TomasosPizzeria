using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Models;



namespace TomasosPizzeria.Controllers
{
    public class MenuController : Controller
    {
        private IMatrattRepository repository;

        public MenuController(IMatrattRepository repo) { repository = repo; }

        public IActionResult ShowMenu()
        {
            var matratter = repository.GetMatratter()
                .Include(m => m.MatrattProdukt)
                .ThenInclude(v => v.Produkt)
                .ToList();
            
            return View(matratter);
        }
    }
}