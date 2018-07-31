using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThesisProcessor.Interfaces;
using ThesisProcessor.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public async Task UpdateThesis(Thesis thesis)
        {
            _dbContext.Attach(thesis);
            _dbContext.Entry(thesis).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Thesis> GetThesis(string id)
        {
            return _dbContext.Theses.First(t => t.Id == id);
        }
    }
}
