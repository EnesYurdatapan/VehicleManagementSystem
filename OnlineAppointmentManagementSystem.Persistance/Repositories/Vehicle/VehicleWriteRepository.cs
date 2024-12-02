using OnlineAppointmentManagementSystem.Application.Repositories;
using OnlineAppointmentManagementSystem.Domain.Entities;
using OnlineAppointmentManagementSystem.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Persistance.Repositories
{
    public class VehicleWriteRepository : WriteRepository<Vehicle>, IVehicleWriteRepository
    {
        public VehicleWriteRepository(AppointmentDbContext appointmentDbContext) : base(appointmentDbContext)
        {
        }
    }
}
