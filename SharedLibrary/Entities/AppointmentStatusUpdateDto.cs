using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class AppointmentStatusUpdateDto
    {
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
    }
}
