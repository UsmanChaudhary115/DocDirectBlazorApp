using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public string? XAccountLink { get; set; } = "#";
        public string? LinkedinAccountLink { get; set; } = "#";
        public string? FacebookAccountLink { get; set; } = "#";
        public string? InstagramAccountLink { get; set; } = "#";
    }
}
