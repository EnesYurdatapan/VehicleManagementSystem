using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Domain.Entities;

namespace VehicleManagementSystem.Application.Abstraction.Services
{
    public interface IVehicleUsageService
    {
        Task<VehicleUsageDto> AddVehicleUsage(VehicleUsageDto vehicleUsage);
        Task<bool> DeleteVehicleUsage(string vehicleUsageId);
        Task<bool> UpdateVehicleUsage(VehicleUsageDto vehicleUsage);
        List<VehicleUsage> GetAllVehicleUsage();
        Task<VehicleUsageDto> GetVehicleUsageById(string id);
    }
}
