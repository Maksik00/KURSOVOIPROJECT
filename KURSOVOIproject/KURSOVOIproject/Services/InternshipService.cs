using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KURSOVOIproject.Data;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly SflDbContext _db;
        public InternshipService(SflDbContext db) => _db = db;

        // Реализация старого метода с фильтрами
        public async Task<List<Internship>> GetAllAsync(
            string? field = null,
            string? location = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            var query = _db.Internships.Include(i => i.Company).AsQueryable();

            if (!string.IsNullOrWhiteSpace(field))
                query = query.Where(i => i.Title.Contains(field));

            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(i => i.Company.City.Contains(location));

            if (from.HasValue)
                query = query.Where(i => i.StartDate >= from.Value);

            if (to.HasValue)
                query = query.Where(i => i.EndDate <= to.Value);

            return await query.ToListAsync();
        }

        public async Task<Internship?> GetByIdAsync(int id) =>
            await _db.Internships
                     .Include(i => i.Company)
                     .SingleOrDefaultAsync(i => i.Id == id);

        public async Task AddAsync(Internship internship)
        {
            await _db.Internships.AddAsync(internship);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Internship internship)
        {
            _db.Internships.Update(internship);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ent = await _db.Internships.FindAsync(id);
            if (ent != null)
            {
                _db.Internships.Remove(ent);
                await _db.SaveChangesAsync();
            }
        }

        // -------------- Реализация дополнительных методов для SearchPage --------------

        public async Task<List<Internship>> GetAllWithCompaniesAsync() =>
            await _db.Internships
                     .Include(i => i.Company)
                     .ToListAsync();

        public async Task<List<Internship>> SearchByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return new List<Internship>();

            return await _db.Internships
                    .Include(i => i.Company)
                    .Where(i => i.Title.Contains(title))
                    .ToListAsync();
        }
    }
}
