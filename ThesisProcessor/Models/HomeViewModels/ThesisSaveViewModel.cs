using Microsoft.AspNetCore.Http;
using System;

namespace ThesisProcessor.Models.HomeViewModels
{
    public class ThesisSaveViewModel
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string References { get; set; }
        public string Author { get; set; }
        public string Supervisor { get; set; }
        public DateTime DateOfThesis { get; set; }
        public IFormFile Thesis { get; set; }

        public Thesis ToEntity()
        {
            return new Thesis
            {
                Title = this.Title,
                Abstract = this.Abstract,
                Author = this.Author,
                Supervisor = this.Supervisor,
                DateModified = DateTime.UtcNow,
                References = References
            };
        }
    }
}
