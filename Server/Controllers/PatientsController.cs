using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Shared.Entities;

namespace Server.Controllers
{
    // Controllers/PatientsController.cs

    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly UserManager<Patient> _userManager;

        public PatientsController(UserManager<Patient> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _userManager.Users.ToListAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(string id)
        {
            var patient = await _userManager.FindByIdAsync(id);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }
        //[HttpPost]
        //public async Task<IActionResult> RegisterPatient(Patient patient)
        //{
        //    _context.Patients.Add(patient);

        //    // Create notification
        //    var notification = new Notification
        //    {
        //        Message = $"New patient registered: {patient.Name}",
        //        Timestamp = DateTime.UtcNow
        //    };
        //    _context.Notifications.Add(notification);

        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }

}
