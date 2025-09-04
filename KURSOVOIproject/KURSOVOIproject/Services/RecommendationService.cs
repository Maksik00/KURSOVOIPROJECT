using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KURSOVOIproject.Data;
using KURSOVOIproject.Models;
using Microsoft.EntityFrameworkCore;

namespace KURSOVOIproject.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly SflDbContext _dbContext;

        public RecommendationService(SflDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Internship>> GetRecommendationsAsync(int studentId)
        {
            // TODO: 
            //  1) Найти студента с его специальностью, навыками, преференциями
            //  2) Подобрать стажировки той же специальности + 
            //     стажировки, содержащие ключевые слова из навыков студента
            //  3) Вернуть упорядоченный список (например, сначала по дате, затем по релевантности)

            var student = await _dbContext.Students
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return new List<Internship>();

            // Простейший пример: все стажировки по той же специализации
            var recommended = await _dbContext.Internships
                .Include(i => i.Company)
                .Where(i => i.IdSpecialization == student.IdSpecialization)
                .ToListAsync();

            return recommended;
        }
    }
}
