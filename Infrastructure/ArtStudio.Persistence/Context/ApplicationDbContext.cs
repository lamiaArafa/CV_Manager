
using CV_Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CV_Manager.Infrastructure.Configuration;

namespace CV_Manager.Infrastructure.Presistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CV> CVs { get; set; }
        public DbSet<PersonalInformation> PersonalInformations { get; set; }
        public DbSet<ExperienceInformation> ExperienceInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetCVConfiguration();
            modelBuilder.SetExperienceInformationConfiguration();
            modelBuilder.SetPersonalInformationConfiguration();
        }
    }
}
