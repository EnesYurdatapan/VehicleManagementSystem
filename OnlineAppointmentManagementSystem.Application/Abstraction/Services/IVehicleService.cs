using OnlineAppointmentManagementSystem.Application.DTOs;
using OnlineAppointmentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Application.Abstraction.Services
{
    public interface IVehicleService
    {
        Task<VehicleDto> AddVehicle(VehicleDto vehicle);
        Task<bool> DeleteVehicle(string vehicleId);
        Task<bool> UpdateVehicle(VehicleDto vehicle);
        List<Vehicle> GetAllVehicles();
        Task<VehicleDto> GetVehicleById(string id);
    }
}
