using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly HospitalDbContext _context;
        private readonly UserManager<Patient> _userManager;

        public AdminController(HospitalDbContext context, UserManager<Patient> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("doctors/{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorById(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // ✅ Get all appointments for a specific doctor
        [HttpGet("doctors/{id}/appointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsForDoctor(int id)
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == id)
                .ToListAsync();

            return appointments;
        }

        [HttpGet("dashboard/stats")]
        public async Task<ActionResult<DashboardStatsDto>> GetDashboardStats()
        {
            var today = DateTime.Today;

            var stats = new DashboardStatsDto
            {
                TotalDoctors = await _context.Doctors.CountAsync(d => d.IsApproved == true),
                AppointmentsToday = await _context.Appointments.CountAsync(a => a.AppointmentDate.Date == today && (a.isDeleted == null || a.isDeleted == false)),
                RegisteredPatients = await _context.Patients.CountAsync(),

                CancelledAppointments = await _context.Appointments.CountAsync(a => a.isDeleted == true)
            };

            return Ok(stats);
        }

        [HttpGet("monthly-appointments")]
        public async Task<ActionResult<IEnumerable<MonthlyAppointmentStats>>> GetMonthlyAppointmentStats()
        {
            var stats = await _context.Appointments
                .Where(a => a.AppointmentDate.Year == DateTime.Now.Year && a.isDeleted == false)
                .GroupBy(a => a.AppointmentDate.Month)
                .Select(g => new MonthlyAppointmentStats
                {
                    Month = g.Key,
                    AppointmentCount = g.Count()
                })
                .ToListAsync();

            return Ok(stats);
        }


        [HttpGet("recent-activity")]
        public async Task<IActionResult> GetRecentActivity()
        {
            var recentAppointments = await _context.Appointments
                .OrderByDescending(a => a.AppointmentDate)
                .Take(5)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();
            return Ok(recentAppointments);
        }
        [HttpPut("appointments/{appointmentId}")]
        public async Task<IActionResult> UpdateAppointment(int appointmentId, [FromBody] Appointment updatedAppointment)
        {
            var existing = await _context.Appointments.FindAsync(appointmentId);
            if (existing == null)
                return NotFound();

            existing.AppointmentDate = updatedAppointment.AppointmentDate;
            existing.Description = updatedAppointment.Description;
            // You may also update patient ID or other fields if needed

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("appointments/{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("patients")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            var patients = await _userManager.Users.ToListAsync();
            return Ok(patients);
        }
        [HttpGet("appointments/all")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();

            return Ok(appointments);
        }
        [HttpPut("appointments/{appointmentId}/status")]
        public async Task<IActionResult> UpdateAppointmentStatus(int appointmentId, [FromBody] AppointmentStatusUpdateDto dto)
        {
            var existing = await _context.Appointments.FindAsync(appointmentId);
            if (existing == null)
                return NotFound();

            existing.isApproved = dto.IsApproved;
            existing.isDeleted = dto.IsDeleted;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("today")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetTodayAppointments()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var appointments = await _context.Appointments
                .Where(a => a.AppointmentDate >= today && a.AppointmentDate < tomorrow)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();

            return Ok(appointments);
        }


    }
}
