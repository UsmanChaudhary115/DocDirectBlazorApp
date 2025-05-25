using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Server.Interfaces;

namespace Server.Repositories
{
    public class AppointmentDTORepository : IAppointmentDTORepository
    {
        private readonly HospitalDbContext _context;
        public AppointmentDTORepository(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task<AppointmentDTO> GetAppointmentDTOByIdAsync(int id)
        {
            return await _context.AppointmentDTOs.FindAsync(id);
        }
        public async Task<IEnumerable<AppointmentDTO>> GetAllAppointmentDTOsAsync()
        {
            return await _context.AppointmentDTOs.ToListAsync();
        }

        public async Task AddAppointmentDTOAsync(AppointmentDTO appointmentDTO)
        {
            await _context.AppointmentDTOs.AddAsync(appointmentDTO);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAppointmentDTOAsync(AppointmentDTO appointmentDTO)
        {
            _context.AppointmentDTOs.Update(appointmentDTO);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAppointmentDTOAsync(int id)
        {
            var appointmentDTO = await _context.AppointmentDTOs.Where(a => a.AppointmentId == id).FirstOrDefaultAsync();
            if (appointmentDTO == null)
            {
                throw new Exception("Appointment not found");
            }
            _context.AppointmentDTOs.Remove(appointmentDTO);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<AppointmentDTO>> GetAppointmentDTOsByPatientIdAsync(string patientId)
        {
            return await _context.AppointmentDTOs
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        } 
        public async Task<IEnumerable<AppointmentDTO>> GetAppointmentDTOsByDateAsync(DateTime date)
        {
            return await _context.AppointmentDTOs
                .Where(a => a.AppointmentDate == date)
                .ToListAsync();
        }
    }
}
