using Shared.Entities;

namespace Shared.Entities
{
    public class PatientIndexViewModel
    {
        public List<Doctor> Doctors { get; set; }
        public List<AppointmentDTO> Appointments { get; set; }
    }
}
