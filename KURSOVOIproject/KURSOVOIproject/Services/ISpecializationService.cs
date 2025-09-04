using System.Collections.Generic;
using System.Threading.Tasks;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public interface ISpecializationService
    {
        Task<List<Specialization>> GetAllAsync();
        Task<Specialization?> GetByIdAsync(int id);
        Task AddAsync(Specialization specialization);
        Task UpdateAsync(Specialization specialization);
        Task DeleteAsync(int id);
    }
}
