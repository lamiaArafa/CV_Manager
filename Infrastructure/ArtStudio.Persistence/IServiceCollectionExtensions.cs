

using CV_Manager.Domain.Entities;
using CV_Manager.Domain.Interfaces.Repository;
using CV_Manager.Infrastructure.Persistence.Repositories;
using CV_Manager.Infrastructure.Repositories;
using CV_Manager.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CV_Manager.Infrastructure.Presistence
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPresistanceServices(this IServiceCollection services) 
        {
         
            services.AddScoped<ICVRepository, CVRepository>();
            services.AddScoped<IPersonalInformationRepository, PersonalInformationRepository>();
            services.AddScoped<IExperienceInformationRepository, ExperienceInformationRepository>();
        }
    }
}
