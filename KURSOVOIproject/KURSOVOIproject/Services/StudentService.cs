using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KURSOVOIproject.Data;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public class StudentService : IStudentService
    {
        private readonly SflDbContext _db;
        public StudentService(SflDbContext db) => _db = db;

        public async Task<List<Student>> GetAllAsync() =>
            await _db.Students
                     .Include(s => s.Specialization)
                     .ToListAsync();

        public async Task<Student?> GetByIdAsync(int id) =>
            await _db.Students
                     .Include(s => s.Specialization)
                     .SingleOrDefaultAsync(s => s.Id == id);

        // ← Новая реализация
        public async Task<Student> GetByPhoneAndPasswordAsync(string phone, string password)
        {
            // Заменили SingleOrDefaultAsync на FirstOrDefaultAsync
            return await _db.Students
                .Where(s => s.TelNumber == phone && s.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Student student)
        {
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _db.Students.Update(student);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ent = await _db.Students.FindAsync(id);
            if (ent != null)
            {
                _db.Students.Remove(ent);
                await _db.SaveChangesAsync();
            }
        }
    }
}
