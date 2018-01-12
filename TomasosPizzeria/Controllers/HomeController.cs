using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.Controllers
{
    public class HomeController : Controller
    {
        private IMatrattRepository repository;

        public HomeController(IMatrattRepository repo) { repository = repo; }

        public IActionResult Index()
        {
            return View();
        }
    }
}