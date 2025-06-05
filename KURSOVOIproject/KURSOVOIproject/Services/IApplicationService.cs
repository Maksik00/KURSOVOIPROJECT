using System.Collections.Generic;
using System.Threading.Tasks;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public interface IApplicationService
    {
        Task<List<ApplicationEntity>> GetAllAsync();
        Task<List<ApplicationEntity>> GetByStudentAsync(int studentId);
        Task<List<ApplicationEntity>> GetByInternshipAsync(int internshipId);
        Task<ApplicationEntity?> GetByIdAsync(int id);
        Task AddAsync(ApplicationEntity application);
        Task UpdateAsync(ApplicationEntity application);
        Task DeleteAsync(int id);
    }
}
