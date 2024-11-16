using OnlineAppointmentManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Domain.Entities
{
    public class Appointment:BaseEntity
    {
        public DateTime AppointmentDate { get; set; }
        public bool Status { get; set; }
    }
}
