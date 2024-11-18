using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Application.DTOs
{
    public class AppointmentDto
    {
        public string Id { get; set; } 
        public string ServiceId { get; set; } 
        public string ServiceName { get; set; } 
        public string AppUserId { get; set; } 
        public DateTime AppointmentDate { get; set; } 
        public string Status { get; set; } 
    }

}
