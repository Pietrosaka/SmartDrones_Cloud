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
    public class IndexModel : PageModel
    {
        private readonly IDroneApiService _droneApiService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IDroneApiService droneApiService, ILogger<IndexModel> logger)
        {
            _droneApiService = droneApiService;
            _logger = logger;
        }

        public IEnumerable<DroneDto>? Drones { get; set; }
        public string? ErrorMessage { get; set; }

        [BindProperty]
        public DroneDto NewDrone { get; set; } = new DroneDto();

        [BindProperty]
        public DroneDto EditedDrone { get; set; } = new DroneDto();

        public async Task OnGetAsync(long? editId)
        {
            try
            {
                Drones = await _droneApiService.GetDronesAsync();
                if (Drones == null)
                {
                    ErrorMessage = "Não foi possível carregar os drones da API.";
                    Drones = new List<DroneDto>();
                }

                if (editId.HasValue)
                {
                    var droneToEdit = Drones.FirstOrDefault(d => d.Id == editId.Value);
                    if (droneToEdit != null)
                    {
                        EditedDrone = droneToEdit;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Drone com ID {editId.Value} não encontrado para edição.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar drones na página.");
                ErrorMessage = "Ocorreu um erro ao tentar carregar os dados. Tente novamente mais tarde.";
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
                NewDrone.Id = 0;
                var createdDrone = await _droneApiService.CreateDroneAsync(NewDrone);
                if (createdDrone == null)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao criar drone na API.");
                    await OnGetAsync(null);
                    return Page();
                }

                ModelState.Clear();
                NewDrone = new DroneDto();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar drone.");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao criar o drone.");
                await OnGetAsync(null);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(EditedDrone.Id);
                return Page();
            }

            try
            {
                var updatedDrone = await _droneApiService.UpdateDroneAsync(EditedDrone.Id, EditedDrone);
                if (updatedDrone == null)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao atualizar drone na API.");
                    await OnGetAsync(EditedDrone.Id);
                    return Page();
                }

                EditedDrone = new DroneDto();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar drone com ID {EditedDrone.Id}.");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao atualizar o drone.");
                await OnGetAsync(EditedDrone.Id);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            try
            {
                var success = await _droneApiService.DeleteDroneAsync(id);
                if (!success)
                {
                    ModelState.AddModelError(string.Empty, $"Erro ao deletar drone com ID {id} na API.");
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar drone com ID {id}.");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao deletar o drone.");
                return RedirectToPage();
            }
        }
    }
}