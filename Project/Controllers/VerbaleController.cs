using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;

namespace Project.Controllers
{
    public class VerbaleController : Controller
    {
        private readonly IVerbaleService _verbaleService;

        public VerbaleController(IVerbaleService verbaleService)
        {
            _verbaleService = verbaleService;
        }

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
                return RedirectToAction("Index", "Home");
            }
            return View(verbale);
        }

        public IActionResult VerbaliByTrasgressore()
        {
            var verbaliByTrasgressore = _verbaleService.GetAllVerbaliByTrasgressore();
            return View(verbaliByTrasgressore);
        }
    }
}
