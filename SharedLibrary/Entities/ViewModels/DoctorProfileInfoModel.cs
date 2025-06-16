using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class DoctorProfileInfoModel
    {
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string WorkedAt { get; set; } = string.Empty;
        public int? DoctorId { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
