using System.Collections.Generic;
using System.Threading.Tasks;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);

        // ← Метод для входа: возвращает null, если не найдено
        Task<Student?> GetByPhoneAndPasswordAsync(string telNumber, string password);

        Task AddAsync(Student student);
        Task CreateAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
}
