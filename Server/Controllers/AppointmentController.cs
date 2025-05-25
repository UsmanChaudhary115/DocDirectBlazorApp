using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentDTORepository _appointmentDTORepository;
        private readonly IDoctorRepository _doctorRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository, IAppointmentDTORepository appointmentDTORepository, IDoctorRepository doctorRepository)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentDTORepository = appointmentDTORepository;
            _doctorRepository = doctorRepository;
        }

        [HttpGet("GetAppointment/{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            try
            {
                var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
                if (appointment == null)
                    return NotFound();

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllAppointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            try
            {
                var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByDoctorId(int doctorId)
        {
            try
            {
                var appointments = await _appointmentRepository.GetAppointmentsByDoctorIdAsync(doctorId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateAppointment")]
        public async Task<ActionResult<Appointment>> CreateAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Save Appointment
                await _appointmentRepository.AddAppointmentAsync(appointment); 

                // Fetch Doctor Name
                var doctor = await _doctorRepository.GetDoctorByIdAsync(appointment.DoctorId); 

                // Create and Save AppointmentDTO
                var dto = new AppointmentDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    PatientId = appointment.PatientId, 
                    AppointmentDate = appointment.AppointmentDate,
                    DoctorName = doctor?.Name ?? "Unknown",
                    Description = appointment.Description ?? "",
                    isApproved = appointment.isApproved ?? false
                };

                await _appointmentDTORepository.AddAppointmentDTOAsync(dto); 

                return CreatedAtAction(nameof(GetAppointment), new { id = appointment.AppointmentId }, appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateAppointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
                if (existingAppointment == null)
                    return NotFound();

                await _appointmentRepository.UpdateAppointmentAsync(appointment);
                
                var dto = new AppointmentDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    PatientId = appointment.PatientId,
                    AppointmentDate = appointment.AppointmentDate,
                    DoctorName = (await _doctorRepository.GetDoctorByIdAsync(appointment.DoctorId))?.Name ?? "Unknown",
                    Description = appointment.Description ?? "",
                    isApproved =  false
                };
                await _appointmentDTORepository.UpdateAppointmentDTOAsync(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                await _appointmentRepository.DeleteAppointmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Appointment not found")
                    return NotFound();

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
