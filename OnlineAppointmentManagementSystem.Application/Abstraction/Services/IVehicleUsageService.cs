using OnlineAppointmentManagementSystem.Application.DTOs;
using OnlineAppointmentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Application.Abstraction.Services
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
