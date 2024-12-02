using Microsoft.EntityFrameworkCore;
using OnlineAppointmentManagementSystem.Application.Abstraction.Services;
using OnlineAppointmentManagementSystem.Application.DTOs;
using OnlineAppointmentManagementSystem.Application.Repositories;
using OnlineAppointmentManagementSystem.Domain.Entities;
using OnlineAppointmentManagementSystem.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Persistance.Services
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

        public async Task<VehicleUsageDto> AddVehicleUsage(VehicleUsageDto vehicleUsage)
        {
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
            await _vehicleUsageWriteRepository.AddAsync(vehicleUsageToAdd);
            await _vehicleUsageWriteRepository.SaveAsync();
            return vehicleUsage;
        }

        public async Task<bool> DeleteVehicleUsage(string vehicleUsageId)
        {
            var vehicleUsageToDelete = await _vehicleUsageReadRepository.GetByIdAsync(vehicleUsageId);
            if (vehicleUsageToDelete != null)
            {
                await _vehicleUsageWriteRepository.DeleteAsync(vehicleUsageToDelete.Id);
                return true;
            }
            return false;
        }

        public List<VehicleUsage> GetAllVehicleUsage()
        {
            return _vehicleUsageReadRepository.GetAll().Include(vu=>vu.Vehicle).ToList();
        }

        public async Task<VehicleUsageDto> GetVehicleUsageById(string id)
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
                return vehicleUsageDto;
            }
            return new VehicleUsageDto();
        }

        public async Task<bool> UpdateVehicleUsage(VehicleUsageDto vehicleUsage)
        {
            var vehicleToUpdate = await _vehicleUsageReadRepository.GetByIdAsync(vehicleUsage.Id);
            if (vehicleToUpdate != null)
            {
                vehicleToUpdate.ActiveHours = vehicleUsage.ActiveHours;
                vehicleToUpdate.MaintenanceHours = vehicleUsage.MaintenanceHours;
                vehicleToUpdate.IdleHours = (7 * 24) - (vehicleUsage.ActiveHours + vehicleUsage.MaintenanceHours);
                vehicleToUpdate.UpdatedDate= DateTime.UtcNow;

                await _vehicleUsageWriteRepository.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
