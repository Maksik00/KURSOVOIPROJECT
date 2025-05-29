using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public interface IInternshipService
    {
        Task<List<Internship>> GetAllAsync(
            string? field = null,
            string? location = null,
            DateTime? from = null,
            DateTime? to = null);

        Task<Internship?> GetByIdAsync(int id);
        Task AddAsync(Internship internship);
        Task UpdateAsync(Internship internship);
        Task DeleteAsync(int id);
    }
}
