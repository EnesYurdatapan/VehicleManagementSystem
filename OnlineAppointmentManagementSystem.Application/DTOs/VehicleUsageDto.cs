using OnlineAppointmentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Application.DTOs
{
    public class VehicleUsageDto
    {
        public string Id { get; set; }
        public string VehicleId { get; set; }
        public double ActiveHours { get; set; }
        public double MaintenanceHours { get; set; }
        public double IdleHours { get; set; }
    }
}
