using CV_Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CV_Manager.Infrastructure.Configuration
{
    public static class PersonalInformationConfiguration
    {
        public static ModelBuilder SetPersonalInformationConfiguration(this ModelBuilder builder)
        {
            builder.Entity<PersonalInformation>(entity =>
            {
                entity.Property(x => x.FullName)
             .IsRequired()
             .HasMaxLength(255);

                entity.Property(x => x.CityName)
                    .HasMaxLength(100);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasAnnotation("EmailAddress", true); 

                entity.Property(x => x.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(15);


            });

            return builder;
        }
    }
}
