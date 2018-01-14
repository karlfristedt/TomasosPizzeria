using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Models;


namespace TomasosPizzeria.Controllers
{
    public class MenuController : Controller
    {
        private IMatrattRepository repository;

        public MenuController(IMatrattRepository repo) { repository = repo; }

        public IActionResult ShowMenu()
        {
            var matratter = repository.Matratter;
            return View(matratter);
        }
    }
}