using System.Collections.Generic;
using System.Threading.Tasks;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
}
