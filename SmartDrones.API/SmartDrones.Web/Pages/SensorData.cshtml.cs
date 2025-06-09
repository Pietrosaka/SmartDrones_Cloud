using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartDrones.Web.Models;
using SmartDrones.Web.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDrones.Web.Pages
{
    public class SensorDataModel : PageModel
    {
        private readonly ISensorDataApiService _sensorDataApiService;
        private readonly IDroneApiService _droneApiService;
        private readonly ILogger<SensorDataModel> _logger;

        public SensorDataModel(ISensorDataApiService sensorDataApiService, IDroneApiService droneApiService, ILogger<SensorDataModel> logger)
        {
            _sensorDataApiService = sensorDataApiService;
            _droneApiService = droneApiService;
            _logger = logger;
        }

        public IEnumerable<SensorDataDto>? SensorDataList { get; set; }
        public IEnumerable<DroneDto>? Drones { get; set; }
        public string? ErrorMessage { get; set; }

        [BindProperty]
        public SensorDataDto NewSensorData { get; set; } = new SensorDataDto();

        [BindProperty]
        public SensorDataDto EditedSensorData { get; set; } = new SensorDataDto();

        public async Task OnGetAsync(long? editId)
        {
            try
            {
                SensorDataList = await _sensorDataApiService.GetSensorDataAsync();
                Drones = await _droneApiService.GetDronesAsync();

                if (SensorDataList == null)
                {
                    ErrorMessage = "Não foi possível carregar os dados de sensor da API.";
                    SensorDataList = new List<SensorDataDto>();
                }
                if (Drones == null)
                {
                    ErrorMessage += (ErrorMessage == null ? "" : " ") + "Não foi possível carregar os drones da API.";
                    Drones = new List<DroneDto>();
                }

                if (editId.HasValue)
                {
                    var sensorDataToEdit = SensorDataList.FirstOrDefault(s => s.Id == editId.Value);
                    if (sensorDataToEdit != null)
                    {
                        EditedSensorData = sensorDataToEdit;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Dado de sensor com ID {editId.Value} não encontrado para edição.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar dados de sensor na página.");
                ErrorMessage = "Ocorreu um erro ao tentar carregar os dados. Tente novamente mais tarde.";
                SensorDataList = new List<SensorDataDto>();
                Drones = new List<DroneDto>();
            }
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(null);
                return Page();
            }

            try
            {
                var createdSensorData = await _sensorDataApiService.CreateSensorDataAsync(NewSensorData);
                if (createdSensorData == null)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao criar dado de sensor na API.");
                    await OnGetAsync(null);
                    return Page();
                }

                NewSensorData = new SensorDataDto();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar dado de sensor.");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao criar o dado de sensor.");
                await OnGetAsync(null);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(EditedSensorData.Id);
                return Page();
            }

            try
            {
                var updatedSensorData = await _sensorDataApiService.UpdateSensorDataAsync(EditedSensorData.Id, EditedSensorData);
                if (updatedSensorData == null)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao atualizar dado de sensor na API.");
                    await OnGetAsync(EditedSensorData.Id);
                    return Page();
                }

                EditedSensorData = new SensorDataDto();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar dado de sensor com ID {EditedSensorData.Id}.");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao atualizar o dado de sensor.");
                await OnGetAsync(EditedSensorData.Id);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            try
            {
                var success = await _sensorDataApiService.DeleteSensorDataAsync(id);
                if (!success)
                {
                    ModelState.AddModelError(string.Empty, $"Erro ao deletar dado de sensor com ID {id} na API.");
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar dado de sensor com ID {id}.");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao deletar o dado de sensor.");
                return RedirectToPage();
            }
        }
    }
}