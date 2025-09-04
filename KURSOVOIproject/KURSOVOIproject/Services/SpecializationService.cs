using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KURSOVOIproject.Data;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly SflDbContext _db;
        public SpecializationService(SflDbContext db) => _db = db;

        public async Task<List<Specialization>> GetAllAsync() =>
            await _db.Specializations
                     .Include(sp => sp.Students)
                     .ToListAsync();

        public async Task<Specialization?> GetByIdAsync(int id) =>
            await _db.Specializations
                     .Include(sp => sp.Students)
                     .SingleOrDefaultAsync(sp => sp.Id == id);

        public async Task AddAsync(Specialization specialization)
        {
            await _db.Specializations.AddAsync(specialization);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Specialization specialization)
        {
            _db.Specializations.Update(specialization);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ent = await _db.Specializations.FindAsync(id);
            if (ent != null)
            {
                _db.Specializations.Remove(ent);
                await _db.SaveChangesAsync();
            }
        }
    }
}
