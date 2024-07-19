using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;

namespace Project.Controllers
{
    public class AnagraficaController : Controller
    {
        private readonly IAnagraficaService _anagraficaService;

        public AnagraficaController(IAnagraficaService anagraficaService)
        {
            _anagraficaService = anagraficaService;
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
                return RedirectToAction("Index", "Home");
            }
            return View(anagrafica);
        }

        public IActionResult TrasgressoreByPuntiDecurtati()
        {
            var trasgressoreByPuntiDecurtati = _anagraficaService.GetAllTrasgressoreByPuntiDecurtati();
            return View(trasgressoreByPuntiDecurtati);
        }
    }
}
