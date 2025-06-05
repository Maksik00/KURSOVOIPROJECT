using System.Collections.Generic;
using System.Threading.Tasks;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public interface ICompanyService
    {
        Task<List<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);

        // Для создания (регистрации) компании
        Task AddAsync(Company company);

        // Для поиска при входе (по имени + паролю)
        Task<Company?> GetByNameAndPasswordAsync(string name, string password);

        Task UpdateAsync(Company company);
        Task DeleteAsync(int id);
    }
}
