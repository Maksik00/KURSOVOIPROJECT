using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KURSOVOIproject.Data;
using Microsoft.EntityFrameworkCore;

namespace KURSOVOIproject.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly SflDbContext _dbContext;

        public StatisticsService(SflDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dictionary<string, int>> GetCountBySpecializationAsync()
        {
            // TODO: Посчитать кол-во стажировок в каждой специализации
            var result = await _dbContext.Internships
                .Include(i => i.Specialization)
                .GroupBy(i => i.Specialization.Name)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .ToListAsync();

            return result.ToDictionary(x => x.Name, x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetTopCompaniesAsync(int topN = 5)
        {
            // TODO: Топ компаний по кол-ву опубликованных стажировок
            var result = await _dbContext.Internships
                .Include(i => i.Company)
                .GroupBy(i => i.Company.Name)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(topN)
                .ToListAsync();

            return result.ToDictionary(x => x.Name, x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetGeneralStatisticsAsync()
        {
            // TODO: Общее количество: студентов, компаний, стажировок
            int studentsCount = await _dbContext.Students.CountAsync();
            int companiesCount = await _dbContext.Companies.CountAsync();
            int internshipsCount = await _dbContext.Internships.CountAsync();

            return new Dictionary<string, int>
            {
                { "Students", studentsCount },
                { "Companies", companiesCount },
                { "Internships", internshipsCount }
            };
        }
    }
}
