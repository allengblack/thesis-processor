using System.Collections.Generic;
using System.Threading.Tasks;
using ThesisProcessor.Models;
using ThesisProcessor.Models.HomeViewModels;

namespace ThesisProcessor.Interfaces
{
    public interface IThesisService
    {
        Task<IEnumerable<Thesis>> GetAllTheses();
        Task SubmitThesis(ThesisSaveViewModel model);
        Task UpdateTheis(ThesisSaveViewModel model);
        Task DeleteThesis(string thesisId, string filename);
    }
}
