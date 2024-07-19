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
        private readonly IViolazioneService _violazioneService;
        private readonly IVerbaleService _verbaleService;

        public HomeController(ILogger<HomeController> logger, IAnagraficaService anagraficaService, IViolazioneService violazioneService, IVerbaleService verbaleService)
        {
            _logger = logger;
            _anagraficaService = anagraficaService;
            _violazioneService = violazioneService;
            _verbaleService = verbaleService;
            
        }

        public IActionResult Index()
        {
            return View();
        }




                                    ////ANAGRAFICA////

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



                                   ////VIOLAZIONI////
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
                return RedirectToAction("Index");
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

        ////VERBALI////
        public IActionResult CreateVerbale()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateVerbale(Verbale verbale)
        {
            if (ModelState.IsValid)
            {
                _verbaleService.Create(verbale);
                return RedirectToAction("Index");
            }
            return View(verbale);
        }


        public IActionResult VerbaliByTrasgressore()
        {
            var verbaliByTrasgressore = _verbaleService.GetAllVerbaliByTrasgressore();
            return View(verbaliByTrasgressore);
        }


        public IActionResult TrasgressoreByPuntiDecurtati()
        {
            var trasgressoreByPuntiDecurtati = _anagraficaService.GetAllTrasgressoreByPuntiDecurtati();
            return View(trasgressoreByPuntiDecurtati);
        }



        //
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
