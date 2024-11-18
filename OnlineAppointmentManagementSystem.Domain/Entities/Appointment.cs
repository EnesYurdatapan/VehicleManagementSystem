using OnlineAppointmentManagementSystem.Domain.Entities.Common;
using OnlineAppointmentManagementSystem.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Domain.Entities
{
    public class Appointment:BaseEntity
    {
        [ForeignKey(nameof(Service))]
        public string ServiceId { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Service Service { get; set; }
        public AppUser AppUser { get; set; }
        public string Status { get; set; }
    }
}
