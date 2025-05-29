using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFLAPI.Data;
using SFLAPI.Models;
using SFLAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class CompaniesController : ControllerBase
{
    private readonly SFLDbContext _db;
    public CompaniesController(SFLDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> Get()
    {
        var list = await _db.Companies
          .Select(c => new CompanyDto
          {
              Id = c.Id,
              Name = c.Name,
              City = c.City,
              Street = c.Street,
              Building = c.Building
          })
          .ToListAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> Get(int id)
    {
        var c = await _db.Companies.FindAsync(id);
        if (c == null) return NotFound();
        return Ok(new CompanyDto
        {
            Id = c.Id,
            Name = c.Name,
            City = c.City,
            Street = c.Street,
            Building = c.Building
        });
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateCompanyDto dto)
    {
        var c = new Company
        {
            Name = dto.Name,
            City = dto.City,
            Street = dto.Street,
            Building = dto.Building,
            Password = dto.Password
        };
        _db.Companies.Add(c);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = c.Id }, null);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, CreateCompanyDto dto)
    {
        var c = await _db.Companies.FindAsync(id);
        if (c == null) return NotFound();
        c.Name = dto.Name;
        c.City = dto.City;
        c.Street = dto.Street;
        c.Building = dto.Building;
        if (!string.IsNullOrEmpty(dto.Password))
            c.Password = dto.Password;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var c = await _db.Companies.FindAsync(id);
        if (c == null) return NotFound();
        _db.Companies.Remove(c);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
