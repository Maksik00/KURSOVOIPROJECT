using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KURSOVOIproject.Data;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly SflDbContext _db;
        public ApplicationService(SflDbContext db) => _db = db;

        public async Task<List<ApplicationEntity>> GetAllAsync() =>
            await _db.Applications
                     .Include(a => a.Student)
                     .Include(a => a.Internship)
                     .ToListAsync();

        public async Task<List<ApplicationEntity>> GetByStudentAsync(int studentId) =>
            await _db.Applications
                     .Include(a => a.Internship)
                     .Where(a => a.IdStudent == studentId)
                     .ToListAsync();

        public async Task<List<ApplicationEntity>> GetByInternshipAsync(int internshipId) =>
            await _db.Applications
                     .Include(a => a.Student)
                     .Where(a => a.IdInternship == internshipId)
                     .ToListAsync();

        public async Task<ApplicationEntity?> GetByIdAsync(int id) =>
            await _db.Applications
                     .Include(a => a.Student)
                     .Include(a => a.Internship)
                     .SingleOrDefaultAsync(a => a.Id == id);

        public async Task AddAsync(ApplicationEntity application)
        {
            _db.Applications.Add(application);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(ApplicationEntity application)
        {
            _db.Applications.Update(application);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ent = await _db.Applications.FindAsync(id);
            if (ent != null)
            {
                _db.Applications.Remove(ent);
                await _db.SaveChangesAsync();
            }
        }
    }
}
