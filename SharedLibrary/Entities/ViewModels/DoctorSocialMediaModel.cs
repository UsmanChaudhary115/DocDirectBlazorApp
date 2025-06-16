using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class DoctorSocialMediaModel
    {
        public string? XAccountLink { get; set; } = "#";
        public string? LinkedinAccountLink { get; set; } = "#";
        public string? FacebookAccountLink { get; set; } = "#";
        public string? InstagramAccountLink { get; set; } = "#";
        public int? DoctorId { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
