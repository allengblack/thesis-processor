using System.Collections.Generic;
using System.Threading.Tasks;
using ThesisProcessor.Models;
using ThesisProcessor.Models.ThesesViewModels;

namespace ThesisProcessor.Interfaces
{
    public interface IThesisService
    {
        Task<IEnumerable<Thesis>> GetAllTheses();
        Task<Thesis> GetThesis(string id);
        Task<Thesis> GetThesisForLoggedInUser();
        Task SubmitThesis(ThesisCreateViewModel model);
        Task UpdateThesis(ThesisSaveViewModel thesis);
        Task ApproveThesis(string id);
        Task DeleteThesis(string thesisId);
        Task ResetThesisApproval(string id);
    }
}
