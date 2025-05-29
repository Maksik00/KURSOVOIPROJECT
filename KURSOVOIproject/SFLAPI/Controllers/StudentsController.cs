using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFLAPI.Data;
using SFLAPI.Models;
using SFLAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SFLAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class StudentsController : ControllerBase
    {
        private readonly SFLDbContext _db;
        public StudentsController(SFLDbContext db) => _db = db;

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> Get()
        {
            var list = await _db.Students
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    TelNumber = s.TelNumber,
                    Course = s.Course,
                    IdSpecialization = s.IdSpecialization
                })
                .ToListAsync();
            return Ok(list);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> Get(int id)
        {
            var s = await _db.Students.FindAsync(id);
            if (s == null) return NotFound();
            return Ok(new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                TelNumber = s.TelNumber,
                Course = s.Course,
                IdSpecialization = s.IdSpecialization
            });
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult> Post(CreateStudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                TelNumber = dto.TelNumber,
                Course = dto.Course,
                IdSpecialization = dto.IdSpecialization,
                Password = dto.Password
            };
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = student.Id }, null);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateStudentDto dto)
        {
            var s = await _db.Students.FindAsync(id);
            if (s == null) return NotFound();
            s.Name = dto.Name;
            s.TelNumber = dto.TelNumber;
            s.Course = dto.Course;
            s.IdSpecialization = dto.IdSpecialization;
            s.Password = dto.Password;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var s = await _db.Students.FindAsync(id);
            if (s == null) return NotFound();
            _db.Students.Remove(s);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
