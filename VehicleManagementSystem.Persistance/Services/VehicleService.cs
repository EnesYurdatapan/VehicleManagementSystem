using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using VehicleManagementSystem.Application.Abstraction.Services;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Application.Repositories;
using VehicleManagementSystem.Domain.Entities;

namespace VehicleManagementSystem.Persistance.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleReadRepository _vehicleReadRepository;
        private readonly IVehicleWriteRepository _vehicleWriteRepository;

        public VehicleService(IVehicleReadRepository vehicleReadRepository, IVehicleWriteRepository vehicleWriteRepository)
        {
            _vehicleReadRepository = vehicleReadRepository;
            _vehicleWriteRepository = vehicleWriteRepository;
        }

        public async Task<VehicleDto> AddVehicle(VehicleDto vehicleDto)
        {
            Vehicle vehicle = new Vehicle()
            {
                Id = Guid.NewGuid().ToString(),
                Name = vehicleDto.Name,
                Plate = vehicleDto.Plate,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
            vehicleDto.Id = vehicle.Id;
            await _vehicleWriteRepository.AddAsync(vehicle);
            await _vehicleWriteRepository.SaveAsync();
            return vehicleDto;
        }

        public async Task<bool> DeleteVehicle(string vehicleId)
        {
            var vehicleToDelete = await _vehicleReadRepository.GetByIdAsync(vehicleId);
            if (vehicleToDelete != null)
            {
                await _vehicleWriteRepository.DeleteAsync(vehicleToDelete.Id);
                await _vehicleWriteRepository.SaveAsync();
            }

            else
                return false;

            return true;
        }

        public List<Vehicle> GetAllVehicles()
        {
            return _vehicleReadRepository.GetAll().ToList();
        }


        public async Task<VehicleDto> GetVehicleById(string id)
        {
            var vehicle = await _vehicleReadRepository.GetByIdAsync(id);
            if (vehicle != null)
            {
                var vehicleDto = new VehicleDto()
                {
                    Plate = vehicle.Plate,
                    Name = vehicle.Name,
                    Id = vehicle.Id,
                };
                return vehicleDto;
            }
            return new VehicleDto();
        }

        public async Task<bool> UpdateVehicle(VehicleDto vehicle)
        {
            var vehicleToUpdate = await _vehicleReadRepository.GetByIdAsync(vehicle.Id);
            if (vehicleToUpdate!=null)
            {
                vehicleToUpdate.Name = vehicle.Name;
                vehicleToUpdate.Plate= vehicle.Plate;
                vehicleToUpdate.UpdatedDate = DateTime.UtcNow;

                await _vehicleWriteRepository.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
