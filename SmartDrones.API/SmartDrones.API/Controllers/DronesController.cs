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
    public class DronesController : ControllerBase
    {
        private readonly IDroneService _droneService;

        public DronesController(IDroneService droneService)
        {
            _droneService = droneService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DroneDto>>> Get()
        {
            var drones = await _droneService.GetAllDronesAsync();
            return Ok(drones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DroneDto>> Get(long id)
        {
            var drone = await _droneService.GetDroneByIdAsync(id);
            if (drone == null)
            {
                return NotFound();
            }
            return Ok(drone);
        }

        [HttpPost]
        public async Task<ActionResult<DroneDto>> Post([FromBody] DroneDto droneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdDrone = await _droneService.CreateDroneAsync(droneDto);
                return CreatedAtAction(nameof(Get), new { id = createdDrone.Id }, createdDrone);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DroneDto>> Put(long id, [FromBody] DroneDto droneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            droneDto.Id = id; 

            try
            {
                var updatedDrone = await _droneService.UpdateDroneAsync(droneDto);
                return Ok(updatedDrone);
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
                await _droneService.DeleteDroneAsync(id);
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