using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Domain.Entities;

namespace VehicleManagementSystem.Application.Abstraction.Services
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
