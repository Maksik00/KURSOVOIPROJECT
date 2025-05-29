using System;
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
    public class ApplicationsController : ControllerBase
    {
        private readonly SFLDbContext _db;
        public ApplicationsController(SFLDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> Get()
        {
            var list = await _db.Applications
                .Select(a => new ApplicationDto
                {
                    Id = a.Id,
                    SubmissionDate = a.SubmissionDate,
                    ResponseDate = a.ResponseDate,
                    IsAccepted = a.IsAccepted,
                    IdStudent = a.IdStudent,
                    IdInternship = a.IdInternship
                })
                .ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDto>> Get(int id)
        {
            var a = await _db.Applications.FindAsync(id);
            if (a == null) return NotFound();
            return Ok(new ApplicationDto
            {
                Id = a.Id,
                SubmissionDate = a.SubmissionDate,
                ResponseDate = a.ResponseDate,
                IsAccepted = a.IsAccepted,
                IdStudent = a.IdStudent,
                IdInternship = a.IdInternship
            });
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> Post(CreateApplicationDto dto)
        {
            var application = new Application
            {
                SubmissionDate = DateTime.Now,
                IdStudent = dto.IdStudent,
                IdInternship = dto.IdInternship,
                IsAccepted = false
            };
            _db.Applications.Add(application);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = application.Id }, null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Company,Admin")]
        public async Task<ActionResult> Put(int id, UpdateApplicationDto dto)
        {
            var a = await _db.Applications.FindAsync(id);
            if (a == null) return NotFound();
            a.ResponseDate = dto.ResponseDate;
            a.IsAccepted = dto.IsAccepted;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var a = await _db.Applications.FindAsync(id);
            if (a == null) return NotFound();
            _db.Applications.Remove(a);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
