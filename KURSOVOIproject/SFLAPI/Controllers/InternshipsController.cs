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
    [Authorize(Roles = "Student,Company,Admin")]
    public class InternshipsController : ControllerBase
    {
        private readonly SFLDbContext _db;
        public InternshipsController(SFLDbContext db) => _db = db;

        [HttpGet]
        [HttpGet]
        [HttpGet]
        [Authorize(Roles = "Student,Company,Admin")]
        public async Task<ActionResult<IEnumerable<InternshipDto>>> Get(
    [FromQuery] string? field = null,
    [FromQuery] string? location = null,
    [FromQuery] DateTime? from = null,
    [FromQuery] DateTime? to = null)
        {
            // Базовый запрос с eager-loading компании
            var query = _db.Internships
                           .Include(i => i.Company)
                           .AsQueryable();

            // Фильтр по названию (сфере)
            if (!string.IsNullOrWhiteSpace(field))
                query = query.Where(i => i.Title.Contains(field));

            // Фильтр по городу компании
            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(i => i.Company.City.Contains(location));

            // Фильтр по датам
            if (from.HasValue)
                query = query.Where(i => i.StartDate >= from.Value);
            if (to.HasValue)
                query = query.Where(i => i.EndDate <= to.Value);

            // Проекция в DTO
            var list = await query
                .Select(i => new InternshipDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    StartDate = i.StartDate,
                    EndDate = i.EndDate,
                    Requirements = i.Requirements,
                    IdCompany = i.IdCompany
                })
                .ToListAsync();

            return Ok(list);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<InternshipDto>> Get(int id)
        {
            var i = await _db.Internships.FindAsync(id);
            if (i == null) return NotFound();
            return Ok(new InternshipDto
            {
                Id = i.Id,
                Title = i.Title,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                Requirements = i.Requirements,
                IdCompany = i.IdCompany
            });
        }

        [HttpPost]
        [Authorize(Roles = "Company,Admin")]
        public async Task<ActionResult> Post(CreateInternshipDto dto)
        {
            var internship = new Internship
            {
                Title = dto.Title,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Requirements = dto.Requirements,
                IdCompany = dto.IdCompany
            };
            _db.Internships.Add(internship);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = internship.Id }, null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Company,Admin")]
        public async Task<ActionResult> Put(int id, UpdateInternshipDto dto)
        {
            var i = await _db.Internships.FindAsync(id);
            if (i == null) return NotFound();
            i.Title = dto.Title;
            i.StartDate = dto.StartDate;
            i.EndDate = dto.EndDate;
            i.Requirements = dto.Requirements;
            i.IdCompany = dto.IdCompany;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Company,Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var i = await _db.Internships.FindAsync(id);
            if (i == null) return NotFound();
            _db.Internships.Remove(i);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
