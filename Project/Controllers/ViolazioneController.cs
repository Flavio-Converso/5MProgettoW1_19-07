using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;

namespace Project.Controllers
{
    public class ViolazioneController : Controller
    {
        private readonly IViolazioneService _violazioneService;

        public ViolazioneController(IViolazioneService violazioneService)
        {
            _violazioneService = violazioneService;
        }

        public IActionResult CreateViolazione()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateViolazione(Violazione violazione)
        {
            if (ModelState.IsValid)
            {
                _violazioneService.Create(violazione);
                return RedirectToAction("Index", "Home");
            }
            return View(violazione);
        }

        public IActionResult GetAllViolazioni()
        {
            var violazioni = _violazioneService.GetAllViolazioni();
            return View(violazioni);
        }

        public IActionResult ViolazioniOver10Punti()
        {
            var violazioni = _violazioneService.GetViolazioneOver10Punti();
            return View(violazioni);
        }

        public IActionResult ViolazioniOver400Importo()
        {
            var violazioni = _violazioneService.GetViolazioneOver400Importo();
            return View(violazioni);
        }
    }
}
