﻿using Microsoft.AspNetCore.Http;
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

        public async Task<Thesis> GetThesis(string id)
        {
            return await _thesisDAL.GetThesis(id);
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
                DateCreated = new DateTime(Convert.ToInt32(model.DateOfThesis.Year), Convert.ToInt32(model.DateOfThesis.Month), 1)
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

        public async Task UpdateTheis(ThesisSaveViewModel model)
        {
            var thesis = new Thesis();
            await _thesisDAL.UpdateThesis(thesis);
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
                {"application/msword", ".doc"},
                {"application/octet-stream", ".docx" },
                {"application/vnd.openxmlformats-officedocument.wordprocessingml.document", ".docx" }
            };
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        }
    }
    #endregion
}