using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisProcessor.Interfaces;
using ThesisProcessor.Models;
using ThesisProcessor.Models.HomeViewModels;

namespace ThesisProcessor.Controllers
{
    public class HomeController : Controller
    {
        private readonly IThesisService _thesisService;

        public HomeController(IThesisService thesisService)
        {
            _thesisService = thesisService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ThesisSaveViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Thesis == null || model.Thesis.Length == 0)
                    return Content("file not selected");

                await _thesisService.SubmitThesis(model);

                return RedirectToAction(nameof(Submit));
            }
            return Content("Error uploading document");
        }

        public async Task<IActionResult> ViewAll()
        {
            var theses = await _thesisService.GetAllTheses();
            return View(theses);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
