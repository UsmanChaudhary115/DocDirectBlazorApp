using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string? PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Description { get; set; }
        public bool? isApproved { get; set; }
        public bool? isDeleted { get; set; }
        public virtual Patient? Patient { get; set; }  // Navigation property for Patient
        public virtual Doctor? Doctor { get; set; }    // Navigation property for Doctor
    }
}
