using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KURSOVOIproject.Data;
using KURSOVOIproject.Models;
using Microsoft.EntityFrameworkCore;

namespace KURSOVOIproject.Services
{
    /// <summary>
    /// Заглушечная реализация IAdminService. Пока без фактической логики, только TODO.
    /// </summary>
    public class AdminService : IAdminService
    {
        private readonly SflDbContext _dbContext;

        public AdminService(SflDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BlockStudentAsync(int studentId)
        {
            // TODO: пометить запись студента как заблокированную, либо удалить
            var student = await _dbContext.Students.FindAsync(studentId);
            if (student != null)
            {
                // например, добавим свойство student.IsBlocked = true, если оно появится
                // TODO: реализовать блокировку
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task BlockCompanyAsync(int companyId)
        {
            // TODO: пометить запись компании как заблокированную
            var company = await _dbContext.Companies.FindAsync(companyId);
            if (company != null)
            {
                // TODO: реализовать блокировку
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Dictionary<string, int>> GetInternshipsCountBySpecializationAsync()
        {
            // TODO: вернуть количество стажировок по названиям специализаций
            var result = new Dictionary<string, int>();

            var data = await _dbContext.Internships
                .Include(i => i.Specialization)
                .GroupBy(i => i.Specialization.Name)
                .Select(g => new { SpecializationName = g.Key, Count = g.Count() })
                .ToListAsync();

            foreach (var item in data)
            {
                result[item.SpecializationName] = item.Count;
            }

            return result;
        }

        public async Task<List<(int CompanyId, string CompanyName, int InternshipCount)>> GetTopCompaniesByInternshipCountAsync(int topN)
        {
            // TODO: вернуть список топ N компаний по количеству созданных ими стажировок
            var data = await _dbContext.Companies
                .Select(c => new
                {
                    CompanyId = c.Id,
                    c.Name,
                    InternshipCount = c.Internships.Count
                })
                .OrderByDescending(x => x.InternshipCount)
                .Take(topN)
                .ToListAsync();

            return data.Select(x => (x.CompanyId, x.Name, x.InternshipCount)).ToList();
        }
    }
}
