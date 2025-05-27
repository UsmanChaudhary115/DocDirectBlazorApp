using Shared.Entities;

namespace Server.Interfaces
{
    public interface IAppointmentDTORepository
    {
        Task AddAppointmentDTOAsync(AppointmentDTO appointmentDTO);
        Task DeleteAppointmentDTOAsync(int id);
        Task<IEnumerable<AppointmentDTO>> GetAllAppointmentDTOsAsync();
        Task<AppointmentDTO> GetAppointmentDTOByIdAsync(int id);
        Task<IEnumerable<AppointmentDTO>> GetAppointmentDTOsByPatientIdAsync(string patientId);
        Task UpdateAppointmentDTOAsync(AppointmentDTO appointmentDTO); 
    }
}