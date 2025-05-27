using Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Microsoft.EntityFrameworkCore; 

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _doctorsRepository; 
		private readonly INotificationService _notificationService;
		public DoctorsController(IDoctorRepository doctorsRepository, INotificationService notificationService)
        {
            _doctorsRepository = doctorsRepository; 
			_notificationService = notificationService;
		} 
		[HttpPost("register")]
		public async Task<IActionResult> RegisterDoctor([FromBody] Doctor doctor)
		{
			Console.WriteLine($"Received doctor: {doctor?.Name}");

			if (!ModelState.IsValid)
			{
				foreach (var state in ModelState)
				{
					foreach (var error in state.Value.Errors)
					{
						Console.WriteLine($"Validation error in {state.Key}: {error.ErrorMessage}");
                    }
                }

				return BadRequest(ModelState);
			}

			await _doctorsRepository.AddDoctorAsync(doctor); 

			await _notificationService.AddNotificationAsync($"New doctor registered: {doctor.Name}");

			return Ok();
		}
        [HttpGet("GetApprovedDoctors")]
		public async Task<ActionResult<IEnumerable<Doctor>>> GetApprovedDoctors()
		{
			try
			{
                var doctors = await _doctorsRepository.GetApprovedDoctors();
                    return Ok(doctors);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
        [HttpGet("GetPendingDoctors")]
		public async Task<ActionResult<IEnumerable<Doctor>>> GetPendingDoctors()
		{
			try
			{
				var doctors = await _doctorsRepository.GetPendingDoctors();
					return Ok(doctors);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("GetAllDoctors")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            try
            {
                var doctors = await _doctorsRepository.GetAllDoctorsAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetDoctorById/{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorById(int id)
        {
            try
            {
                var doctor = await _doctorsRepository.GetDoctorByIdAsync(id);
                if (doctor == null)
                    return NotFound();
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("CreateDoctor")]
        public async Task<ActionResult<Doctor>> CreateDoctor([FromBody] Doctor doctor)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _doctorsRepository.AddDoctorAsync(doctor);
                return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.DoctorId }, doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] Doctor doctor)
        {
            if (id != doctor.DoctorId)
                return BadRequest("ID mismatch");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _doctorsRepository.UpdateDoctorAsync(doctor);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                await _doctorsRepository.DeleteDoctorAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetDoctorsBySpecialization/{specialization}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsBySpecialization(string specialization)
        {
            try
            {
                var doctors = await _doctorsRepository.GetDoctorsBySpecializationAsync(specialization);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
