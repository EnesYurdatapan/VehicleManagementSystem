using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using VehicleManagementSystem.Application.Abstraction.Services;
using VehicleManagementSystem.Application.Constants;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Application.Repositories;
using VehicleManagementSystem.Application.Results;
using VehicleManagementSystem.Domain.Entities;
using VehicleManagementSystem.Persistance.Repositories;

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

        public async Task<IDataResult<VehicleDto>> AddVehicle(VehicleDto vehicleDto)
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
            bool result = await _vehicleWriteRepository.AddAsync(vehicle);
            if (result)
            {
                await _vehicleWriteRepository.SaveAsync();
                return new SuccessDataResult<VehicleDto>(vehicleDto, Messages.VehicleSuccessfullyAddedMessage);
            }
            return new ErrorDataResult<VehicleDto>(vehicleDto, Messages.VehicleCouldntAddedMessage);
        }

        public async Task<IResult> DeleteVehicle(string vehicleId)
        {
            var vehicleToDelete = await _vehicleReadRepository.GetByIdAsync(vehicleId);
            if (vehicleToDelete != null)
            {
                bool result = await _vehicleWriteRepository.DeleteAsync(vehicleToDelete.Id);
                if (result)
                {
                    await _vehicleWriteRepository.SaveAsync();
                    return new SuccessResult(Messages.VehicleSuccessfullyDeletedMessage);
                }
            }

            return new ErrorResult(Messages.VehicleCouldntDeletedMessage);

        }

        public IDataResult<List<VehicleDto>> GetAllVehicles()
        {
            var vehicles = _vehicleReadRepository
               .GetAll()
               .ToList();

            var vehicleDtos = vehicles.Select(vu => new VehicleDto
            {
                Id = vu.Id,
                Plate = vu.Plate,
                Name= vu.Name,
            }).ToList();
            return new SuccessDataResult<List<VehicleDto>>(vehicleDtos);
        }


        public async Task<IDataResult<VehicleDto>> GetVehicleById(string id)
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
                return new SuccessDataResult<VehicleDto>(vehicleDto);
            }
            return new ErrorDataResult<VehicleDto>();
        }

        public async Task<IResult> UpdateVehicle(VehicleDto vehicle)
        {
            var vehicleToUpdate = await _vehicleReadRepository.GetByIdAsync(vehicle.Id);
            if (vehicleToUpdate != null)
            {
                vehicleToUpdate.Name = vehicle.Name;
                vehicleToUpdate.Plate = vehicle.Plate;
                vehicleToUpdate.UpdatedDate = DateTime.UtcNow;

                await _vehicleWriteRepository.SaveAsync();
                return new SuccessResult(Messages.VehicleSuccessfullyUpdatedMessage);
            }
            return new ErrorResult(Messages.VehicleCouldntUpdatedMessage);
        }
    }
}
