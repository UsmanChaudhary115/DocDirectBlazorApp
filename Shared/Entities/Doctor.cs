using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
namespace Shared.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Specialization { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;
        public string WorkedAt { get; set; } = string.Empty;
        public string Password { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public string? XAccountLink { get; set; } = "#";
        public string? LinkedinAccountLink { get; set; } = "#";
        public string? FacebookAccountLink { get; set; } = "#";
        public string? InstagramAccountLink { get; set; } = "#";
    }
}
