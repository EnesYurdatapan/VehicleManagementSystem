using OnlineAppointmentManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Domain.Entities
{
    public class VehicleUsage : BaseEntity
    {

        [ForeignKey(nameof(Vehicle))]
        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public double ActiveHours { get; set; }
        public double MaintenanceHours { get; set; }
        public double IdleHours { get; set; }
    }
}
