using SmartDrones.Web.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace SmartDrones.Web.Services
{
    public class DroneApiService : IDroneApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DroneApiService> _logger;

        public DroneApiService(HttpClient httpClient, ILogger<DroneApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<DroneDto>?> GetDronesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Drones");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<DroneDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro de requisição HTTP ao obter drones.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro de desserialização JSON ao obter drones.");
                return null;
            }
        }

        public async Task<DroneDto?> GetDroneByIdAsync(long id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Drones/{id}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DroneDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Erro de requisição HTTP ao obter drone com ID {id}.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Erro de desserialização JSON ao obter drone com ID {id}.");
                return null;
            }
        }

        public async Task<DroneDto?> CreateDroneAsync(DroneDto drone)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(drone), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Drones", jsonContent);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DroneDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro de requisição HTTP ao criar drone.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro de serialização/desserialização JSON ao criar drone.");
                return null;
            }
        }

        public async Task<DroneDto?> UpdateDroneAsync(long id, DroneDto drone)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(drone), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Drones/{id}", jsonContent);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DroneDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Erro de requisição HTTP ao atualizar drone com ID {id}.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Erro de serialização/desserialização JSON ao atualizar drone com ID {id}.");
                return null;
            }
        }

        public async Task<bool> DeleteDroneAsync(long id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Drones/{id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Erro de requisição HTTP ao deletar drone com ID {id}.");
                return false;
            }
        }
    }
}