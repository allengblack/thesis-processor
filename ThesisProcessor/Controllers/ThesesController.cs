using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ThesisProcessor.Interfaces;
using ThesisProcessor.Models;
using ThesisProcessor.Models.ThesesViewModels;
using static ThesisProcessor.Constants.Constants;

namespace ThesisProcessor.Controllers
{
    public class ThesesController : Controller
    {
        private readonly IThesisService _thesisService;
        private readonly IMapper _mapper;

        public ThesesController(IThesisService thesisService, IMapper mapper)
        {
            _thesisService = thesisService;
            _mapper = mapper;
        }

        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ThesisCreateViewModel model)
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

        public async Task<IActionResult> Reject(string id)
        {
            var thesis = await _thesisService.GetThesis(id);
            var thesisModel = _mapper.Map<ThesisSaveViewModel>(thesis);
            return View(thesisModel);
        }

        public async Task<IActionResult> Approve(string id)
        {
            await _thesisService.ApproveThesis(id);
            return RedirectToAction(nameof(ViewAll));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(ThesisSaveViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _thesisService.UpdateThesis(model);
                return RedirectToAction(nameof(ViewAll));
            }
            else
            {
                return Content("Error saving form");
            }
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