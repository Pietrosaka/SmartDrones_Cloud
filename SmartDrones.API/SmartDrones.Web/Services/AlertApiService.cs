using SmartDrones.Web.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace SmartDrones.Web.Services
{
    public class AlertApiService : IAlertApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AlertApiService> _logger;

        public AlertApiService(HttpClient httpClient, ILogger<AlertApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<AlertDto>?> GetAlertsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Alerts");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<AlertDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro de requisição HTTP ao obter alertas.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro de desserialização JSON ao obter alertas.");
                return null;
            }
        }

        public async Task<AlertDto?> GetAlertByIdAsync(long id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Alerts/{id}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AlertDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Erro de requisição HTTP ao obter alerta com ID {id}.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Erro de desserialização JSON ao obter alerta com ID {id}.");
                return null;
            }
        }

        public async Task<AlertDto?> CreateAlertAsync(AlertDto alert)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(alert), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Alerts", jsonContent);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AlertDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro de requisição HTTP ao criar alerta.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro de serialização/desserialização JSON ao criar alerta.");
                return null;
            }
        }

        public async Task<AlertDto?> UpdateAlertAsync(long id, AlertDto alert)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(alert), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Alerts/{id}", jsonContent);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AlertDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Erro de requisição HTTP ao atualizar alerta com ID {id}.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Erro de serialização/desserialização JSON ao atualizar alerta com ID {id}.");
                return null;
            }
        }

        public async Task<bool> DeleteAlertAsync(long id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Alerts/{id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Erro de requisição HTTP ao deletar alerta com ID {id}.");
                return false;
            }
        }
    }
}