using System.Collections.Generic;
using System.Threading.Tasks;
using ThesisProcessor.Models;

namespace ThesisProcessor.Interfaces
{
    public interface IThesisDAL
    {
        Task<IEnumerable<Thesis>> GetAllThesesAsync();
        Task<Thesis> GetThesis(string id);
        Task<Thesis> GetThesisForLoggedInUser(string id);
        Task SubmitThesis(Thesis thesis);
        Task UpdateThesis(Thesis thesis);
        Task ApproveThesis(string id);
        Task DeleteThesis(string thesisId);
    }
}
