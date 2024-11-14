using CV_Manager.Application.Infrastructure.External;
using CV_Manager.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CV_Manager.Infrastructure.External
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IWhatsAppService, WhatsAppService>();
        }
    }
}
