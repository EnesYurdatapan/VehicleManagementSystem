using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Application.Results;
using VehicleManagementSystem.Domain.Entities;

namespace VehicleManagementSystem.Application.Abstraction.Services
{
    public interface IVehicleUsageService
    {
        Task<IDataResult<VehicleUsageDto>> AddVehicleUsage(VehicleUsageDto vehicleUsage);
        Task<IResult> DeleteVehicleUsage(string vehicleUsageId);
        Task<IResult> UpdateVehicleUsage(VehicleUsageDto vehicleUsage);
        IDataResult<List<VehicleUsageDto>> GetAllVehicleUsage();
        Task<IDataResult<VehicleUsageDto>> GetVehicleUsageById(string id);
    }
}
