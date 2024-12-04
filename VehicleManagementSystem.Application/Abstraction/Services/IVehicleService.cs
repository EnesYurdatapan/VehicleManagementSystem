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
    public interface IVehicleService
    {
        Task<IDataResult<VehicleDto>> AddVehicle(VehicleDto vehicle);
        Task<IResult> DeleteVehicle(string vehicleId);
        Task<IResult> UpdateVehicle(VehicleDto vehicle);
        IDataResult<List<VehicleDto>> GetAllVehicles();
        Task<IDataResult<VehicleDto>> GetVehicleById(string id);
    }
}
