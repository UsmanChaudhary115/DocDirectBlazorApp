using Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Server.Interfaces;
using System.Globalization;

namespace Server.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalDbContext _context;
        public DoctorRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateDoctorProfileInfo(DoctorProfileInfoModel doctor)
        {
            if (doctor == null)
            {
                return false;
            }

            var existingDoctor = await _context.Doctors.FindAsync(doctor.DoctorId);
            if (existingDoctor == null || existingDoctor.Password != doctor.Password)
            {
                return false;
            }

            existingDoctor.Name = doctor.Name;
            existingDoctor.Email = doctor.Email;
            existingDoctor.Specialization = doctor.Specialization;
            existingDoctor.Country = doctor.Country;
            existingDoctor.Education = doctor.Education;
            existingDoctor.Experience = doctor.Experience;
            existingDoctor.Gender = doctor.Gender;
            existingDoctor.WorkedAt = doctor.WorkedAt; 
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateDoctorSocialInfo(DoctorSocialMediaModel doctor)
        {
            if (doctor == null)
            {
                return false;
            }

            var existingDoctor = await _context.Doctors.FindAsync(doctor.DoctorId);
            if (existingDoctor == null || existingDoctor.Password != doctor.Password)
            {
                return false;
            }

            existingDoctor.FacebookAccountLink = doctor.FacebookAccountLink;
            existingDoctor.InstagramAccountLink = doctor.InstagramAccountLink;
            existingDoctor.XAccountLink = doctor.XAccountLink;
            existingDoctor.LinkedinAccountLink = doctor.LinkedinAccountLink;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateDoctorSecurityInfo(DoctorSecurityModel doctor)
        {
            if (doctor == null)
            {
                return false;
            }

            var existingDoctor = await _context.Doctors.FindAsync(doctor.DoctorId);
            if (existingDoctor == null || existingDoctor.Password != doctor.OldPassword)
            {
                return false;
            }

            existingDoctor.Password = doctor.NewPassword; 
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetDoctorByEmailAndPassword(string email, string password)
        {
            return await _context.Doctors
                     .Where(d => d.Email == email && d.Password == password)
                     .Select(d => d.DoctorId)
                     .FirstOrDefaultAsync(); 
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
        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors
                .Where(d => d.DoctorId == id)
                .Include(d => d.Appointments.Where(a => a.isApproved == true && a.isDeleted != true))
                    .ThenInclude(a => a.Patient)
                .Select(d => new Doctor
                {
                    DoctorId = d.DoctorId,
                    Name = d.Name,
                    Email = d.Email,
                    Specialization = d.Specialization,
                    Country = d.Country,
                    Education = d.Education,
                    Experience = d.Experience,
                    Gender = d.Gender,
                    WorkedAt = d.WorkedAt,
                    IsApproved = d.IsApproved,
                    Password = d.Password,
                    XAccountLink = d.XAccountLink,
                    LinkedinAccountLink = d.LinkedinAccountLink,
                    FacebookAccountLink = d.FacebookAccountLink,
                    InstagramAccountLink = d.InstagramAccountLink,
                    Appointments = d.Appointments.ToList()
                })
                .FirstOrDefaultAsync();

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
