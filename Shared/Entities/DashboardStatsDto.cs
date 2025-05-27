using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class DashboardStatsDto
    {
        public int TotalDoctors { get; set; }
        public int AppointmentsToday { get; set; }
        public int RegisteredPatients { get; set; }
        public int CancelledAppointments { get; set; }
    }
}
