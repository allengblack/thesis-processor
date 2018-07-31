using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ThesisProcessor.Interfaces;
using ThesisProcessor.Models;
using ThesisProcessor.Models.HomeViewModels;
using static ThesisProcessor.Constants.Constants;


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

        public async Task<IActionResult> Download(string id)
        {
            var thesis = await _thesisService.GetThesis(id);
            if (thesis != null)
            {
                var path = Path.Combine(PATH, thesis.FileName);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, GetContentType(path), Path.GetFileName(path));
            }
            return Content("File not found.");
        }

        public IActionResult Reject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Reject(ThesisRejectViewModel model)
        {
            return View();
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

        #region private
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        #endregion
    }
}
