using CV_Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CV_Manager.Infrastructure.Configuration
{
    public static class ExperienceInformationConfiguration
    {
        public static ModelBuilder SetExperienceInformationConfiguration(this ModelBuilder builder)
        {
            builder.Entity<ExperienceInformation>(entity =>
            {
                entity.HasKey(ei => ei.Id);

                entity.Property(ei => ei.CompanyName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(ei => ei.City)
                    .HasMaxLength(100); 

                entity.Property(ei => ei.CompanyField)
                    .HasMaxLength(100); 
            });
            return builder;
        }
    }
}
