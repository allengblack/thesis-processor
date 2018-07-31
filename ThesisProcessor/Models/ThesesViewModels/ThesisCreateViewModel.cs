using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ThesisProcessor.Models.ThesesViewModels
{
    public class ThesisCreateViewModel
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string References { get; set; }
        public string Author { get; set; }
        public string Supervisor { get; set; }
        public string FileName { get; set; }
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date of Submission")]
        public DateTime DateOfThesis { get; set; }
        public IFormFile Thesis { get; set; }
    }
}
