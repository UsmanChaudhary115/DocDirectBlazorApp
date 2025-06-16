using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;

namespace Shared.Entities
{
    public class DoctorViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string WorkedAt { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public string? XAccountLink { get; set; } = "#";
        public string? LinkedinAccountLink { get; set; } = "#";
        public string? FacebookAccountLink { get; set; } = "#";
        public string? InstagramAccountLink { get; set; } = "#";
    }
}
