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

        public DbSet<Student> Students { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<ApplicationEntity> Applications { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Student → Specialization (1-many)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Specialization)
                .WithMany(sp => sp.Students)
                .HasForeignKey(s => s.IdSpecialization)
                .OnDelete(DeleteBehavior.Restrict);

            // Internship → Company (1-many)
            modelBuilder.Entity<Internship>()
                .HasOne(i => i.Company)
                .WithMany(c => c.Internships)
                .HasForeignKey(i => i.IdCompany)
                .OnDelete(DeleteBehavior.Cascade);

            // Application → Student (1-many)
            modelBuilder.Entity<ApplicationEntity>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Applications)
                .HasForeignKey(a => a.IdStudent)
                .OnDelete(DeleteBehavior.Cascade);

            // Application → Internship (1-many)
            modelBuilder.Entity<ApplicationEntity>()
                .HasOne(a => a.Internship)
                .WithMany(i => i.Applications)
                .HasForeignKey(a => a.IdInternship)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
