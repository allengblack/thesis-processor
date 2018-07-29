using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThesisProcessor.Interfaces;
using ThesisProcessor.Models;
using System.Linq;

namespace ThesisProcessor.Data
{
    public class ThesisDAL : IThesisDAL
    {
        private ApplicationDbContext _dbContext;

        public async Task<IEnumerable<Thesis>> GetAllThesesAsync()
        {
            return _dbContext.Theses.ToList();
        }

        public ThesisDAL(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task SubmitThesis(Thesis thesis)
        {
            _dbContext.Theses.Add(thesis);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteThesis(string thesisId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateThesis(Thesis thesis)
        {
            throw new NotImplementedException();
        }
    }
}
