using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KURSOVOIproject.Data;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly SflDbContext _db;
        public CompanyService(SflDbContext db) => _db = db;

        public async Task<List<Company>> GetAllAsync() =>
            await _db.Companies
                     .Include(c => c.Internships)
                     .ToListAsync();

        public async Task<Company?> GetByIdAsync(int id) =>
            await _db.Companies
                     .Include(c => c.Internships)
                     .SingleOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Company company)
        {
            _db.Companies.Add(company);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _db.Companies.Update(company);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ent = await _db.Companies.FindAsync(id);
            if (ent != null)
            {
                _db.Companies.Remove(ent);
                await _db.SaveChangesAsync();
            }
        }
    }
}
