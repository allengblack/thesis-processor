using Microsoft.AspNetCore.Http;

namespace ThesisProcessor.Models.HomeViewModels
{
    public class ThesisSaveViewModel
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string References { get; set; }
        public string Author { get; set; }
        public string Supervisor { get; set; }
        public IFormFile Thesis { get; set; }
    }
}
