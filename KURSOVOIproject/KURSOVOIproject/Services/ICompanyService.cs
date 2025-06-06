// Services/ICompanyService.cs

using System.Collections.Generic;
using System.Threading.Tasks;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public interface ICompanyService
    {
        Task<Company?> GetByIdAsync(int id);
        Task<Company?> GetByNameAndPasswordAsync(string name, string password);
        Task<IEnumerable<Company>> GetAllAsync();

        // TODO: Codex, добавь сюда метод для регистрации новой компании:
        Task CreateAsync(Company company);

        // TODO: Codex, добавь сюда метод для обновления данных компании:
        Task UpdateAsync(Company company);

        // TODO: Codex, добавь сюда метод для удаления компании по Id:
        Task DeleteAsync(int id);
        // TODO: можно добавить методы для получения откликов на стажировки компании
    }
}
