using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ThesisProcessor.Interfaces;
using ThesisProcessor.Models;
using ThesisProcessor.Models.HomeViewModels;
using static ThesisProcessor.Constants.Constants;

namespace ThesisProcessor.Services
{
    public class ThesisService : IThesisService
    {
        private readonly IThesisDAL _thesisDAL;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public ThesisService(IThesisDAL thesisDAL, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _thesisDAL = thesisDAL;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<IEnumerable<Thesis>> GetAllTheses()
        {
            return await _thesisDAL.GetAllThesesAsync();
        }

        public async Task SubmitThesis(ThesisSaveViewModel model)
        {
            var user = await GetCurrentUserAsync();
            var filename = $"{model.Author}-{model.Title}{GetFileExtension(model.Thesis.ContentType)}";

            var thesis = new Thesis
            {
                Title = model.Title,
                Author = model.Author,
                Abstract = model.Abstract,
                Supervisor = model.Supervisor,
                References = model.References,
                FileName = filename,
                UploaderId = user.Id,
                DateCreated = DateTime.UtcNow
            };

            using (var stream = new FileStream(Path.Combine(PATH, filename), FileMode.Create))
            {
                await model.Thesis.CopyToAsync(stream);
            }
            await _thesisDAL.SubmitThesis(thesis);
        }

        public async Task DeleteThesis(string thesisId, string filename)
        {
            File.Delete(PATH + filename);
            await _thesisDAL.DeleteThesis(thesisId);
        }

        public Task UpdateTheis(ThesisSaveViewModel model)
        {
            throw new System.NotImplementedException();
        }

        #region PrivateMethods
        private string GetFileExtension(string contentType)
        {
            var types = GetExtensions();
            return types[contentType];
        }

        private Dictionary<string, string> GetExtensions()
        {
            return new Dictionary<string, string>
            {
                {"application/pdf", ".pdf"},
                {"application/octet-stream", ".doc"},
            };
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        }
    }
    #endregion
}
