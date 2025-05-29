using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFLAPI.Data;
using SFLAPI.DTOs;
using SFLAPI.Models;

namespace SFLAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class SpecializationsController : ControllerBase
    {
        private readonly SFLDbContext _db;
        public SpecializationsController(SFLDbContext db) => _db = db;

        [HttpGet]
        [Authorize(Roles = "Student,Company,Admin")]
        public async Task<ActionResult<IEnumerable<SpecializationDto>>> Get()
        {
            var list = await _db.Specializations
                .Select(s => new SpecializationDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Student,Company,Admin")]
        public async Task<ActionResult<SpecializationDto>> Get(int id)
        {
            var s = await _db.Specializations.FindAsync(id);
            if (s == null) return NotFound();
            return Ok(new SpecializationDto
            {
                Id = s.Id,
                Name = s.Name
            });
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateSpecializationDto dto)
        {
            var s = new Specialization
            {
                Name = dto.Name
            };
            _db.Specializations.Add(s);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = s.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateSpecializationDto dto)
        {
            var s = await _db.Specializations.FindAsync(id);
            if (s == null) return NotFound();
            s.Name = dto.Name;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var s = await _db.Specializations.FindAsync(id);
            if (s == null) return NotFound();
            _db.Specializations.Remove(s);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
