
using ArtStudio.Application.Features.Interfaces;

namespace ArtStudio.Web.Hosted
{
    public class WhatsAppHostingService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory; // Add this
        private Timer? _timer;
        private IEnrollmentService? _enrollmentService;

        public WhatsAppHostingService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory; // Initialize the IServiceScopeFactory
        }

        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _enrollmentService = _scopeFactory.CreateAsyncScope().ServiceProvider.GetRequiredService<IEnrollmentService>();

            _timer = new Timer(a => _enrollmentService.SendExpirationMessageAsync().GetAwaiter(), null, TimeSpan.Zero, TimeSpan.FromHours(4));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
