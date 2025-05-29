using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SFLAPI.Data;
using SFLAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SFLAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly SFLDbContext _db;
        private readonly IConfiguration _cfg;

        public AuthService(SFLDbContext db, IConfiguration cfg)
        {
            _db = db;
            _cfg = cfg;
        }

        public async Task RegisterAsync(string name, string password, string role)
        {
            if (role == "Student")
            {
                var stud = new Student
                {
                    Name = name,
                    Password = password,
                    // остальные поля можно заполнить значениями по умолчанию
                };
                _db.Students.Add(stud);
            }
            else if (role == "Company")
            {
                var comp = new Company
                {
                    Name = name,
                    Password = password,
                    // остальные поля по умолчанию
                };
                _db.Companies.Add(comp);
            }
            else
            {
                throw new ArgumentException("Недопустимая роль");
            }

            await _db.SaveChangesAsync();
        }

        public async Task<bool> ValidateAsync(string name, string password, string role)
        {
            if (role == "Student")
            {
                var stud = await _db.Students
                    .SingleOrDefaultAsync(s => s.Name == name);
                return stud != null && stud.Password == password;
            }
            else if (role == "Company")
            {
                var comp = await _db.Companies
                    .SingleOrDefaultAsync(c => c.Name == name);
                return comp != null && comp.Password == password;
            }

            return false;
        }

        public Task<string> GenerateTokenAsync(string name, string role)
        {
            var jwt = _cfg.GetSection("Jwt");
            var keyBytes = Encoding.UTF8.GetBytes(jwt["Key"]);
            var key = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpireMinutes"])),
                signingCredentials: creds);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
