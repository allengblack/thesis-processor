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
            var dbThesis = _dbContext.Theses.First(t => t.Id == thesis.Id);
            dbThesis.RejectReason = thesis.RejectReason;
            dbThesis.Approved = false;

            _dbContext.Entry(dbThesis).Property(x => x.Approved).IsModified = true;
            _dbContext.Entry(dbThesis).Property(x => x.RejectReason).IsModified = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task ApproveThesis(string id)
        {
            var dbThesis = _dbContext.Theses.First(t => t.Id == id);
            dbThesis.Approved = true;
            _dbContext.Entry(dbThesis).Property(x => x.Approved).IsModified = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Thesis> GetThesis(string id)
        {
            return _dbContext.Theses.First(t => t.Id == id);
        }
    }
}
