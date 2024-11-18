using OnlineAppointmentManagementSystem.Web.Utility;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAppointmentManagementSystem.Web.Models
{
    public class Appointment
    {
        public string Id { get; set; }
        [ForeignKey(nameof(Service))]
        public string ServiceId { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Service Service { get; set; }
        public AppUser AppUser { get; set; }
        public string Status { get; set; } = StaticDetails.Waiting;
    }
}
