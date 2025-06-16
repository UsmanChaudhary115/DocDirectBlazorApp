using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class DoctorSecurityModel
    {
        public int? DoctorId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; } 

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
        public string? OldPassword { get; set; }
    }
}
