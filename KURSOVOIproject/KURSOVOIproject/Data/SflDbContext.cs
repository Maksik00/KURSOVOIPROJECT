// Data/SflDbContext.cs
using Microsoft.EntityFrameworkCore;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Data
{
    public class SflDbContext : DbContext
    {
        public SflDbContext(DbContextOptions<SflDbContext> options)
            : base(options)
        {
        }

        // Основные таблицы
        public DbSet<Student> Students { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<ApplicationEntity> Applications { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1) Связь Student → Specialization (1-many)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Specialization)
                .WithMany(sp => sp.Students)
                .HasForeignKey(s => s.IdSpecialization)
                .OnDelete(DeleteBehavior.Restrict);

            // 2) Связь Internship → Company (1-many)
            modelBuilder.Entity<Internship>()
                .HasOne(i => i.Company)
                .WithMany(c => c.Internships)
                .HasForeignKey(i => i.IdCompany)
                .OnDelete(DeleteBehavior.Cascade);

            // 3) Связь ApplicationEntity → Student (1-many)
            modelBuilder.Entity<ApplicationEntity>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Applications)
                .HasForeignKey(a => a.IdStudent)
                .OnDelete(DeleteBehavior.Cascade);

            // 4) Связь ApplicationEntity → Internship (1-many)
            modelBuilder.Entity<ApplicationEntity>()
                .HasOne(a => a.Internship)
                .WithMany(i => i.Applications)
                .HasForeignKey(a => a.IdInternship)
                .OnDelete(DeleteBehavior.Cascade);

            // 5) Данные-миграция: изначальный набор специализаций
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { Id = 1, Name = "Программирование" },
                new Specialization { Id = 2, Name = "Правоведение" },
                new Specialization { Id = 3, Name = "Коммерция" },
                new Specialization { Id = 4, Name = "Бухгалтерия" },
                new Specialization { Id = 5, Name = "Логистика" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
