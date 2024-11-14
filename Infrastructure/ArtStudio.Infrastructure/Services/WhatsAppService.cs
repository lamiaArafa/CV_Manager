using CV_Manager.Application.DTOs.WhatsApp.Requests;
using CV_Manager.Application.Infrastructure.External;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CV_Manager.Infrastructure.Services
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly ILogger<WhatsAppService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string? Url;
        private readonly string? token;

        public WhatsAppService(ILogger<WhatsAppService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            token = "8xH9nfv68KdBqfbMAzCQlXnNaYd9QlzHlFcz0nKb8nvvCwOU8s65aZ8QPDvL";
            Url = "http://whats-pro.net/backend/public/index.php/api/messages/send";
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> SendAsync(WhatsAppRequest request)
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", token);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var data = JsonSerializer.Serialize(request, options);
                _logger.LogInformation("Request Data: {data}", data);

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(Url, content);



                if (!response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Send Whatsapp Message Failed. Response: {responseBody}", responseBody);
                    return false;
                }

                _logger.LogInformation($"Sending ==> End successfully");

                return true;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("HTTP request error: {ex}", ex);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred: {ex}", ex);
                return false;
            }
        }

    }
}
