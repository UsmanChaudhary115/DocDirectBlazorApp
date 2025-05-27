using Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentDTOController : ControllerBase
    {
        private readonly IAppointmentDTORepository _repository;

        public AppointmentDTOController(IAppointmentDTORepository repository)
        {
            _repository = repository;
        }

        // GET: api/AppointmentDTO/{id}
        [HttpGet("GetAppointmentDTO/{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointmentDTO(int id)
        {
            try
            {
                var appointmentDTO = await _repository.GetAppointmentDTOByIdAsync(id);
                if (appointmentDTO == null)
                {
                    return NotFound();
                }
                return Ok(appointmentDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/AppointmentDTO
        [HttpGet("GetAllAppointmentDTOs")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAllAppointmentDTOs()
        {
            try
            {
                var appointmentDTOs = await _repository.GetAllAppointmentDTOsAsync();
                return Ok(appointmentDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/AppointmentDTO/patient/{patientId}
        [HttpGet("GetAppointmentDTOsByPatientId/{patientId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointmentDTOsByPatientId(string patientId)
        {
            try
            {
                var appointmentDTOs = await _repository.GetAppointmentDTOsByPatientIdAsync(patientId);
                return Ok(appointmentDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/AppointmentDTO
        [HttpPost("CreateAppointmentDTO")]
        public async Task<ActionResult<AppointmentDTO>> CreateAppointmentDTO([FromBody] AppointmentDTO appointmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _repository.AddAppointmentDTOAsync(appointmentDTO);
                return CreatedAtAction(nameof(GetAppointmentDTO), new { id = appointmentDTO.Id }, appointmentDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/AppointmentDTO/{id}
        [HttpPut("UpdateAppointmentDTO/{id}")]
        public async Task<IActionResult> UpdateAppointmentDTO(int id, [FromBody] AppointmentDTO appointmentDTO)
        {
            if (id != appointmentDTO.Id)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingAppointment = await _repository.GetAppointmentDTOByIdAsync(id);
                if (existingAppointment == null)
                {
                    return NotFound();
                }

                await _repository.UpdateAppointmentDTOAsync(appointmentDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/AppointmentDTO/{id}
        [HttpDelete("DeleteAppointmentDTO/{id}")]
        public async Task<IActionResult> DeleteAppointmentDTO(int id)
        {
            try
            {
                await _repository.DeleteAppointmentDTOAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Appointment not found")
                {
                    return NotFound();
                }
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
