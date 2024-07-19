using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;
using System.Diagnostics;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnagraficaService _anagraficaService;

        public HomeController(ILogger<HomeController> logger, IAnagraficaService anagraficaService)
        {
            _logger = logger;
            _anagraficaService = anagraficaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateAnagrafica()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAnagrafica(Anagrafica anagrafica)
        {
            if (ModelState.IsValid)
            {
                _anagraficaService.Create(anagrafica);
                return RedirectToAction("Index");
            }
            return View(anagrafica);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
