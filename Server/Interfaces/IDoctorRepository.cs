using Shared.Entities;

namespace Server.Interfaces
{
    public interface IDoctorRepository
    {
        Task AddDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(int id);
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int id);
        Task UpdateDoctorAsync(Doctor doctor);
        Task<IEnumerable<Doctor>> GetDoctorsBySpecializationAsync(string specialization);
        Task<IEnumerable<Doctor>> GetApprovedDoctors();

		Task<IEnumerable<Doctor>> GetPendingDoctors();
	}
}