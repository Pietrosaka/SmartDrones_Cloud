using SmartDrones.Web.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace SmartDrones.Web.Services
{
    public class SensorDataApiService : ISensorDataApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SensorDataApiService> _logger;

        public SensorDataApiService(HttpClient httpClient, ILogger<SensorDataApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<SensorDataDto>?> GetSensorDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/SensorData");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<SensorDataDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro de requisição HTTP ao obter dados de sensor.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro de desserialização JSON ao obter dados de sensor.");
                return null;
            }
        }

        public async Task<SensorDataDto?> GetSensorDataByIdAsync(long id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SensorData/{id}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SensorDataDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Erro de requisição HTTP ao obter dado de sensor com ID {id}.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Erro de desserialização JSON ao obter dado de sensor com ID {id}.");
                return null;
            }
        }

        public async Task<SensorDataDto?> CreateSensorDataAsync(SensorDataDto sensorData)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(sensorData), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/SensorData", jsonContent);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SensorDataDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro de requisição HTTP ao criar dado de sensor.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro de serialização/desserialização JSON ao criar dado de sensor.");
                return null;
            }
        }

        public async Task<SensorDataDto?> UpdateSensorDataAsync(long id, SensorDataDto sensorData)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(sensorData), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/SensorData/{id}", jsonContent);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SensorDataDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Erro de requisição HTTP ao atualizar dado de sensor com ID {id}.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Erro de serialização/desserialização JSON ao atualizar dado de sensor com ID {id}.");
                return null;
            }
        }

        public async Task<bool> DeleteSensorDataAsync(long id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/SensorData/{id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Erro de requisição HTTP ao deletar dado de sensor com ID {id}.");
                return false;
            }
        }
    }
}