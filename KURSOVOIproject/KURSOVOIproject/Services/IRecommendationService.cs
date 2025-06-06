using System.Collections.Generic;
using System.Threading.Tasks;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public interface IRecommendationService
    {
        // TODO: Возвращает список стажировок, рекомендованных данному студенту
        Task<IEnumerable<Internship>> GetRecommendationsAsync(int studentId);
    }
}
