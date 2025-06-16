using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities 
{
    public class PatientInfoModel
    {
        public string userId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Disease { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
