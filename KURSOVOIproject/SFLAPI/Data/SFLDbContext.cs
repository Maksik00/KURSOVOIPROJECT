using Microsoft.EntityFrameworkCore;
using SFLAPI.Models;

namespace SFLAPI.Data
{
    public class SFLDbContext : DbContext
    {
        public SFLDbContext(DbContextOptions<SFLDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Specialization)
                .WithMany(sp => sp.Students)
                .HasForeignKey(s => s.IdSpecialization)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Internship>()
                .HasOne(i => i.Company)
                .WithMany(c => c.Internships)
                .HasForeignKey(i => i.IdCompany)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Applications)
                .HasForeignKey(a => a.IdStudent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Internship)
                .WithMany(i => i.Applications)
                .HasForeignKey(a => a.IdInternship)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
