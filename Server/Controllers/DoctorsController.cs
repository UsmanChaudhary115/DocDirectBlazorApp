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

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] DoctorProfileInfoModel doctor)
        {
            var result = await _doctorsRepository.UpdateDoctorProfileInfo(doctor);
            if (!result)
                return BadRequest("Invalid data or password mismatch.");

            return Ok("Profile information updated successfully.");
        }

        [HttpPut("UpdateSocial")]
        public async Task<IActionResult> UpdateSocial([FromBody] DoctorSocialMediaModel doctor)
        {
            var result = await _doctorsRepository.UpdateDoctorSocialInfo(doctor);
            if (!result)
                return BadRequest("Invalid data or password mismatch.");

            return Ok("Social media links updated successfully.");
        }

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] DoctorSecurityModel doctor)
        {
            var result = await _doctorsRepository.UpdateDoctorSecurityInfo(doctor);
            if (!result)
                return BadRequest("Old password incorrect or update failed.");

            return Ok("Password updated successfully.");
        }

        [HttpPost("register")]
		public async Task<IActionResult> RegisterDoctor([FromBody] DoctorRegisterModel doctorModel)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (doctorModel == null)
            {
                return BadRequest("Invalid data.");
            }
            var doctor = new Doctor
            {
                Name = doctorModel.Name,
                Specialization = doctorModel.Specialization,
                Email = doctorModel.Email,
                Password = doctorModel.Password
            };

			await _doctorsRepository.AddDoctorAsync(doctor); 

			await _notificationService.AddNotificationAsync($"New doctor registered: {doctor.Name}");

			return Ok();
		}
        
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] DoctorSignInModel doctor)
        {
            if (doctor == null || string.IsNullOrEmpty(doctor.Email) || string.IsNullOrEmpty(doctor.Password))
            {
                return BadRequest("Email and password are required.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _doctorsRepository.GetDoctorByEmailAndPassword(doctor.Email, doctor.Password);

            if (result == 0)
            {
                return BadRequest("Invalid email or password.");
            }
            return Ok(result);
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
        public async Task<ActionResult<DoctorViewModel>> GetDoctorById(int id)
        {
            try
            {
                var doctor = await _doctorsRepository.GetDoctorByIdAsync(id);
                if (doctor == null)
                    return NotFound();
                DoctorViewModel doctorViewModel = new DoctorViewModel
                {
                    Name = doctor.Name,
                    Email = doctor.Email,
                    Specialization = doctor.Specialization,
                    Country = doctor.Country,
                    Education = doctor.Education,
                    Experience = doctor.Experience,
                    Gender = doctor.Gender,
                    WorkedAt = doctor.WorkedAt,
                    IsApproved = doctor.IsApproved,

                    Appointments = doctor.Appointments ?? new List<Appointment>(),
                    XAccountLink = doctor.XAccountLink, 
                    LinkedinAccountLink = doctor.LinkedinAccountLink,
                    FacebookAccountLink = doctor.FacebookAccountLink,
                    InstagramAccountLink = doctor.InstagramAccountLink
                };

                return Ok(doctorViewModel);
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
