﻿using OnlineAppointmentManagementSystem.Application.Repositories;
using OnlineAppointmentManagementSystem.Domain.Entities;
using OnlineAppointmentManagementSystem.Persistance.Context;
using OnlineAppointmentManagementSystem.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Persistance.Repositories
{
    public class VehicleUsageReadRepository : ReadRepository<VehicleUsage>, IVehicleUsageReadRepository
    {
        public VehicleUsageReadRepository(AppointmentDbContext appointmentDbContext) : base(appointmentDbContext)
        {
        }
    }
}
