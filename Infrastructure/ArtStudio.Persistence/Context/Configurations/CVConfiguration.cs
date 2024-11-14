using CV_Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CV_Manager.Infrastructure.Configuration
{
    public static class CVConfiguration
    {
        public static ModelBuilder SetCVConfiguration(this ModelBuilder builder)
        {
            builder.Entity<CV>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
                entity.HasOne(d => d.PersonalInformation)  
                        .WithOne()                            
                        .HasForeignKey<CV>(c => c.PersonalInformationId)  
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_cv_personal_information");

                entity.HasOne(d => d.ExperienceInformation)
                        .WithOne()
                        .HasForeignKey<CV>(c => c.ExperienceInformationId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_cv_experience_information");

            });

            return builder;
        }
    }
}