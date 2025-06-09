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
    public class SensorDataController : ControllerBase
    {
        private readonly ISensorDataService _sensorDataService;

        public SensorDataController(ISensorDataService sensorDataService)
        {
            _sensorDataService = sensorDataService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorDataDto>>> Get()
        {
            var sensorData = await _sensorDataService.GetAllSensorDataAsync();
            return Ok(sensorData);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorDataDto>> Get(long id)
        {
            var sensorData = await _sensorDataService.GetSensorDataByIdAsync(id);
            if (sensorData == null)
            {
                return NotFound();
            }
            return Ok(sensorData);
        }

        [HttpGet("drone/{droneId}")]
        public async Task<ActionResult<IEnumerable<SensorDataDto>>> GetByDroneId(long droneId)
        {
            var sensorData = await _sensorDataService.GetSensorDataByDroneIdAsync(droneId);
            if (sensorData == null || !((List<SensorDataDto>)sensorData).Any())
            {
                return NotFound($"Nenhum dado de sensor encontrado para o Drone ID {droneId}.");
            }
            return Ok(sensorData);
        }

        [HttpPost]
        public async Task<ActionResult<SensorDataDto>> Post([FromBody] SensorDataDto sensorDataDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdSensorData = await _sensorDataService.AddSensorDataAsync(sensorDataDto);
                return CreatedAtAction(nameof(Get), new { id = createdSensorData.Id }, createdSensorData);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SensorDataDto>> Put(long id, [FromBody] SensorDataDto sensorDataDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sensorDataDto.Id = id; 

            try
            {
                var updatedSensorData = await _sensorDataService.UpdateSensorDataAsync(sensorDataDto);
                return Ok(updatedSensorData);
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
                await _sensorDataService.DeleteSensorDataAsync(id);
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