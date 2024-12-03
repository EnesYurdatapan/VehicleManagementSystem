using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Domain.Entities.Common;

namespace VehicleManagementSystem.Domain.Entities
{
    public class Vehicle:BaseEntity
    {
        public string Name { get; set; }
        public string Plate { get; set; }
    }
}
