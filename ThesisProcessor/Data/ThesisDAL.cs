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
            return await _dbContext.Theses.ToListAsync();
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

        public async Task DeleteThesis(string thesisId)
        {
            var dbThesis = await _dbContext.Theses.FirstOrDefaultAsync(t => t.Id == thesisId);
            _dbContext.Entry(dbThesis).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateThesis(Thesis thesis)
        {
            var dbThesis = await _dbContext.Theses.FirstOrDefaultAsync(t => t.Id == thesis.Id);
            dbThesis.RejectReason = thesis.RejectReason;
            dbThesis.Approved = false;

            _dbContext.Entry(dbThesis).Property(x => x.Approved).IsModified = true;
            _dbContext.Entry(dbThesis).Property(x => x.RejectReason).IsModified = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task ApproveThesis(string id)
        {
            var dbThesis = await _dbContext.Theses.FirstOrDefaultAsync(t => t.Id == id);
            dbThesis.Approved = true;
            _dbContext.Entry(dbThesis).Property(x => x.Approved).IsModified = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Thesis> GetThesis(string id)
        {
            return await _dbContext.Theses.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Thesis> GetThesisForLoggedInUser(string id)
        {
            return await _dbContext.Theses.FirstOrDefaultAsync(t => t.UploaderId == id);
        }

        public async Task ResetThesisApproval(string id)
        {
            var dbThesis = await _dbContext.Theses.FirstOrDefaultAsync(t => t.Id == id);
            dbThesis.Approved = false;
            dbThesis.RejectReason = null;
            _dbContext.Entry(dbThesis).Property(x => x.Approved).IsModified = true;
            _dbContext.Entry(dbThesis).Property(x => x.RejectReason).IsModified = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
