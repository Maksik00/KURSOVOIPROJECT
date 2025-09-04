using System.Collections.Generic;
using System.Threading.Tasks;
using KURSOVOIproject.ViewModels; // если ViewModel для статистики нужна

namespace KURSOVOIproject.Services
{
    public interface IStatisticsService
    {
        // TODO: Получить количество стажировок по специализациям
        Task<Dictionary<string, int>> GetCountBySpecializationAsync();

        // TODO: Получить топ–5 компаний по кол-ву опубликованных стажировок
        Task<Dictionary<string, int>> GetTopCompaniesAsync(int topN = 5);

        // TODO: Получить общую статистику (количество студентов, компаний, стажировок)
        Task<Dictionary<string, int>> GetGeneralStatisticsAsync();
    }
}
