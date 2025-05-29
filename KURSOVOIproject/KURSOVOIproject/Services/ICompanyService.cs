using System.Collections.Generic;
using System.Threading.Tasks;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public interface ICompanyService
    {
        Task<List<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(int id);
    }
}
