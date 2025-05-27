namespace Shared.Entities
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Description { get; set; }
        public bool isApproved { get; set; }
    }
}
