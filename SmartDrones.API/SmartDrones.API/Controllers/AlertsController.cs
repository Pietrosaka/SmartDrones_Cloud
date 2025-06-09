using Microsoft.AspNetCore.Mvc;
using SmartDrones.Application.DTOs;
using SmartDrones.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertService _alertService;

        public AlertsController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertDto>>> Get()
        {
            var alerts = await _alertService.GetAllAlertsAsync();
            return Ok(alerts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlertDto>> Get(long id)
        {
            var alert = await _alertService.GetAlertByIdAsync(id);
            if (alert == null)
            {
                return NotFound();
            }
            return Ok(alert);
        }

        [HttpPost]
        public async Task<ActionResult<AlertDto>> Post([FromBody] AlertDto alertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdAlert = await _alertService.CreateAlertAsync(alertDto);
                return CreatedAtAction(nameof(Get), new { id = createdAlert.Id }, createdAlert);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AlertDto>> Put(long id, [FromBody] AlertDto alertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            alertDto.Id = id;

            try
            {
                var updatedAlert = await _alertService.UpdateAlertAsync(alertDto);
                return Ok(updatedAlert);
            }
            catch (ApplicationException ex)
            {
                if (ex.Message.Contains("não encontrado"))
                {
                    return NotFound(new { message = ex.Message });
                }
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/resolve")]
        public async Task<ActionResult<AlertDto>> Resolve(long id)
        {
            try
            {
                var resolvedAlert = await _alertService.ResolveAlertAsync(id);
                return Ok(resolvedAlert);
            }
            catch (ApplicationException ex)
            {
                if (ex.Message.Contains("não encontrado"))
                {
                    return NotFound(new { message = ex.Message });
                }
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _alertService.DeleteAlertAsync(id);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                if (ex.Message.Contains("não encontrado"))
                {
                    return NotFound(new { message = ex.Message });
                }
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}