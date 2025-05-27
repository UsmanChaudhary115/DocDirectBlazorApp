using Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Server.Interfaces;

namespace Server.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalDbContext _context;
        public DoctorRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task AddDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }
        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecializationAsync(string specialization)
        {
            return await _context.Doctors.Where(d => d.Specialization == specialization).ToListAsync();
        }
        public async Task<IEnumerable<Doctor>> GetApprovedDoctors()
		{
			return await _context.Doctors.Where(d => d.IsApproved == true).ToListAsync();
		}
        public async Task<IEnumerable<Doctor>> GetPendingDoctors()
		{
			return await _context.Doctors.Where(d => d.IsApproved == false).ToListAsync();
		}
    }
}
