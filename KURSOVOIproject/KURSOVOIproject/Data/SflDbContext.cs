using System;
using Microsoft.EntityFrameworkCore;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Data
{
    /// <summary>
    /// Контекст базы данных SFL. Создаёт все таблицы при первом запуске.
    /// </summary>
    public class SflDbContext : DbContext
    {
        // Конструктор для внедрения через DI
        public SflDbContext(DbContextOptions<SflDbContext> options)
            : base(options)
        {
            // При старте приложения автоматически создаём базу и таблицы, если их нет
            Database.EnsureCreated();
        }

        // Параметрless конструктор нужен, например, для design-time (EF tools)
        public SflDbContext()
        {
            // Настройка SQLite по умолчанию в локальном каталоге приложения
            var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var fullPath = System.IO.Path.Combine(dbPath, "sfl.db");

            var optionsBuilder = new DbContextOptionsBuilder<SflDbContext>();
            optionsBuilder.UseSqlite($"Data Source={fullPath}");

            // Применяем настройки
            this.Options = optionsBuilder.Options;

            // Создаём БД и таблицы
            Database.EnsureCreated();
        }

        // Храним опции, чтобы EF correctly сконфигурировался
        public DbContextOptions<SflDbContext> Options { get; }

        // Списки сущностей (таблицы)
        public DbSet<Student> Students { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<ApplicationEntity> Applications { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Admin> Admins { get; set; }
<<<<<<< HEAD
=======

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Если не настроено через DI — настраиваем SQLite самостоятельно
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var fullPath = System.IO.Path.Combine(dbPath, "sfl.db");
                optionsBuilder.UseSqlite($"Data Source={fullPath}");
            }
        }
>>>>>>> 359d440 (InternshipADDING!)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация связей

            // Student → Specialization (многие к одному)
            // связь студент→специализация
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Specialization)
                .WithMany(sp => sp.Students)
                .HasForeignKey(s => s.IdSpecialization)
                .OnDelete(DeleteBehavior.Restrict);

            // связь стажировка→компания (было)
            modelBuilder.Entity<Internship>()
                .HasOne(i => i.Company)
                .WithMany(c => c.Internships)
                .HasForeignKey(i => i.IdCompany)
                .OnDelete(DeleteBehavior.Cascade);

            // <<--- новая связь стажировка→специализация
            modelBuilder.Entity<Internship>()
                .HasOne(i => i.Specialization)
                .WithMany(sp => sp.Internships)
                .HasForeignKey(i => i.IdSpecialization)
                .OnDelete(DeleteBehavior.Restrict);

            // ApplicationEntity → Student
            modelBuilder.Entity<ApplicationEntity>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Applications)
                .HasForeignKey(a => a.IdStudent)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationEntity → Internship
            modelBuilder.Entity<ApplicationEntity>()
                .HasOne(a => a.Internship)
                .WithMany(i => i.Applications)
                .HasForeignKey(a => a.IdInternship)
                .OnDelete(DeleteBehavior.Cascade);

            // Начальные данные для специализаций
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { Id = 1, Name = "Программирование" },
                new Specialization { Id = 2, Name = "Правоведение" },
                new Specialization { Id = 3, Name = "Коммерция" },
                new Specialization { Id = 4, Name = "Бухгалтерия" },
                new Specialization { Id = 5, Name = "Логистика" }
            );
        }
    }
}
