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
    public class AlertsModel : PageModel
    {
        private readonly IAlertApiService _alertApiService;
        private readonly IDroneApiService _droneApiService;
        private readonly ILogger<AlertsModel> _logger;

        public AlertsModel(IAlertApiService alertApiService, IDroneApiService droneApiService, ILogger<AlertsModel> logger)
        {
            _alertApiService = alertApiService;
            _droneApiService = droneApiService;
            _logger = logger;
        }

        public IEnumerable<AlertDto>? Alerts { get; set; }
        public IEnumerable<DroneDto>? Drones { get; set; }
        public string? ErrorMessage { get; set; }

        [BindProperty]
        public AlertDto NewAlert { get; set; } = new AlertDto();

        [BindProperty]
        public AlertDto EditedAlert { get; set; } = new AlertDto();

        public async Task OnGetAsync(long? editId)
        {
            try
            {
                Alerts = await _alertApiService.GetAlertsAsync();
                Drones = await _droneApiService.GetDronesAsync();

                if (Alerts == null)
                {
                    ErrorMessage = "Não foi possível carregar os alertas da API.";
                    Alerts = new List<AlertDto>();
                }
                if (Drones == null)
                {
                    ErrorMessage += (ErrorMessage == null ? "" : " ") + "Não foi possível carregar os drones da API.";
                    Drones = new List<DroneDto>();
                }

                if (editId.HasValue)
                {
                    var alertToEdit = Alerts.FirstOrDefault(a => a.Id == editId.Value);
                    if (alertToEdit != null)
                    {
                        EditedAlert = alertToEdit;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Alerta com ID {editId.Value} não encontrado para edição.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar alertas na página.");
                ErrorMessage = "Ocorreu um erro ao tentar carregar os dados. Tente novamente mais tarde.";
                Alerts = new List<AlertDto>();
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
                var createdAlert = await _alertApiService.CreateAlertAsync(NewAlert);
                if (createdAlert == null)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao criar alerta na API.");
                    await OnGetAsync(null);
                    return Page();
                }

                NewAlert = new AlertDto();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar alerta.");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao criar o alerta.");
                await OnGetAsync(null);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(EditedAlert.Id);
                return Page();
            }

            try
            {
                var updatedAlert = await _alertApiService.UpdateAlertAsync(EditedAlert.Id, EditedAlert);
                if (updatedAlert == null)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao atualizar alerta na API.");
                    await OnGetAsync(EditedAlert.Id);
                    return Page();
                }

                EditedAlert = new AlertDto();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar alerta com ID {EditedAlert.Id}.");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao atualizar o alerta.");
                await OnGetAsync(EditedAlert.Id);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            try
            {
                var success = await _alertApiService.DeleteAlertAsync(id);
                if (!success)
                {
                    ModelState.AddModelError(string.Empty, $"Erro ao deletar alerta com ID {id} na API.");
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar alerta com ID {id}.");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao deletar o alerta.");
                return RedirectToPage();
            }
        }
    }
}