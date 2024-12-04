using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Application.Abstraction.Services;
using VehicleManagementSystem.Application.Constants;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Application.Repositories;
using VehicleManagementSystem.Application.Results;
using VehicleManagementSystem.Domain.Entities;

namespace VehicleManagementSystem.Persistance.Services
{
    public class VehicleUsageService : IVehicleUsageService
    {
        private readonly IVehicleUsageWriteRepository _vehicleUsageWriteRepository;
        private readonly IVehicleUsageReadRepository _vehicleUsageReadRepository;

        public VehicleUsageService(IVehicleUsageWriteRepository vehicleUsageWriteRepository, IVehicleUsageReadRepository vehicleUsageReadRepository)
        {
            _vehicleUsageWriteRepository = vehicleUsageWriteRepository;
            _vehicleUsageReadRepository = vehicleUsageReadRepository;
        }

        public async Task<IDataResult<VehicleUsageDto>> AddVehicleUsage(VehicleUsageDto vehicleUsage)
        {
            var x = _vehicleUsageReadRepository.GetAll().Include(vu=>vu.Vehicle).Where(vu => vu.Vehicle.Plate == vehicleUsage.VehiclePlate).Include(vu => vu.Vehicle);
            if (x!=null)
            {
                if (vehicleUsage.MaintenanceHours > (7 * 24) || vehicleUsage.ActiveHours > (7 * 24) || vehicleUsage.MaintenanceHours + vehicleUsage.ActiveHours > (7 * 24))
                    return new ErrorDataResult<VehicleUsageDto>(Messages.VehicleUsageHoursErrorMessage);
                var idleHours = (7 * 24) - (vehicleUsage.ActiveHours + vehicleUsage.MaintenanceHours);
                var vehicleUsageToAdd = new VehicleUsage
                {
                    ActiveHours = vehicleUsage.ActiveHours,
                    MaintenanceHours = vehicleUsage.MaintenanceHours,
                    IdleHours = idleHours,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    VehicleId = vehicleUsage.VehicleId,
                    Id = Guid.NewGuid().ToString(),
                };
                bool result = await _vehicleUsageWriteRepository.AddAsync(vehicleUsageToAdd);
                if (result)
                {
                    await _vehicleUsageWriteRepository.SaveAsync();
                    return new SuccessDataResult<VehicleUsageDto>(vehicleUsage, Messages.VehicleUsageSuccessfullyEnteredMessage);

                }
                return new ErrorDataResult<VehicleUsageDto>(vehicleUsage, Messages.VehicleUsageCouldntEnteredMessage);
            }
            return new ErrorDataResult<VehicleUsageDto>(Messages.TheCarAlreadyHadVehicleUsageError);
           
        }

        public async Task<IResult> DeleteVehicleUsage(string vehicleUsageId)
        {
            var vehicleUsageToDelete = await _vehicleUsageReadRepository.GetByIdAsync(vehicleUsageId);
            if (vehicleUsageToDelete != null)
            {
                bool result = await _vehicleUsageWriteRepository.DeleteAsync(vehicleUsageToDelete.Id);
                if (result)
                {
                    await _vehicleUsageWriteRepository.SaveAsync();
                    return new SuccessResult(Messages.VehicleUsageSuccessfullyDeletedMessage);
                }
            }
            return new ErrorResult(Messages.VehicleUsageCouldntDeletedMessage);
        }

        public IDataResult<List<VehicleUsageDto>> GetAllVehicleUsage()
        {
            var vehicleUsages = _vehicleUsageReadRepository
                .GetAll()
                .Include(vu => vu.Vehicle)  // Araç bilgileri de dahil ediliyor
                .ToList();

            var vehicleUsageDtos = vehicleUsages.Select(vu => new VehicleUsageDto
            {
                Id = vu.Id,  // BaseEntity'den gelen ID
                VehicleId = vu.VehicleId,
                ActiveHours = vu.ActiveHours,
                MaintenanceHours = vu.MaintenanceHours,
                IdleHours = vu.IdleHours,
                VehicleName = vu.Vehicle.Name,
                VehiclePlate = vu.Vehicle.Plate
            }).ToList();

            return new SuccessDataResult<List<VehicleUsageDto>>(vehicleUsageDtos);
        }

        public async Task<IDataResult<VehicleUsageDto>> GetVehicleUsageById(string id)
        {
            var vehicleUsage = await _vehicleUsageReadRepository.GetByIdAsync(id);
            if (vehicleUsage != null)
            {
                var vehicleUsageDto = new VehicleUsageDto
                {
                    Id = id,
                    ActiveHours = vehicleUsage.ActiveHours,
                    IdleHours = vehicleUsage.IdleHours,
                    MaintenanceHours = vehicleUsage.MaintenanceHours,
                    VehicleId = vehicleUsage.VehicleId,
                };
                return new SuccessDataResult<VehicleUsageDto>(vehicleUsageDto);
            }
            return new ErrorDataResult<VehicleUsageDto>();
        }

        public async Task<IResult> UpdateVehicleUsage(VehicleUsageDto vehicleUsage)
        {
            var vehicleToUpdate = await _vehicleUsageReadRepository.GetByIdAsync(vehicleUsage.Id);
            if (vehicleToUpdate != null)
            {
                vehicleToUpdate.ActiveHours = vehicleUsage.ActiveHours;
                vehicleToUpdate.MaintenanceHours = vehicleUsage.MaintenanceHours;
                vehicleToUpdate.IdleHours = (7 * 24) - (vehicleUsage.ActiveHours + vehicleUsage.MaintenanceHours);
                vehicleToUpdate.UpdatedDate = DateTime.UtcNow;

                await _vehicleUsageWriteRepository.SaveAsync();
                return new SuccessResult(Messages.VehicleUsageSuccessfullyUpdatedMessage);
            }
            return new ErrorResult(Messages.VehicleUsageCouldntUpdatedMessage);
        }
    }
}
