using Microsoft.EntityFrameworkCore;
using OnlineAppointmentManagementSystem.Application.Abstraction.Services;
using OnlineAppointmentManagementSystem.Application.DTOs;
using OnlineAppointmentManagementSystem.Application.Repositories;
using OnlineAppointmentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Persistance.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentReadRepository _readRepository;
        private readonly IServiceManager _serviceManager;
        private readonly IAppointmentWriteRepository _writeRepository;

        public AppointmentService(IAppointmentReadRepository readRepository, IAppointmentWriteRepository writeRepository, IServiceManager serviceManager)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _serviceManager = serviceManager;
        }

        public async Task<AppointmentDto> AddAppointment(AppointmentDto appointmentDto)
        {
            Appointment appointment = new Appointment
            {
                AppointmentDate = appointmentDto.AppointmentDate,
                AppUserId = appointmentDto.AppUserId,
                ServiceId = appointmentDto.ServiceId,
                Status = "WAITING",
                Id = appointmentDto.Id,
            };

            await _writeRepository.AddAsync(appointment);
            await _writeRepository.SaveAsync();




            var service = await _serviceManager.GetById(appointmentDto.ServiceId);
            appointmentDto.ServiceName = service.Name;
            return appointmentDto;

        }

        public async Task<bool> ChangeStatusOfAppointment(Appointment appointment)
        {
            var appointmentToUpdate = await _readRepository.GetByIdAsync(appointment.Id);
            appointmentToUpdate.Status = appointment.Status;
            await _writeRepository.SaveAsync();
            return true;

        }

        public async Task<bool> DeleteAppointment(string appointmentId)
        {
            var appointmentToDelete = await _readRepository.GetByIdAsync(appointmentId);
            await _writeRepository.DeleteAsync(appointmentToDelete.Id);
            await _writeRepository.SaveAsync();
            return true;
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _readRepository.GetAll().Include(a => a.Service).Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Appointment> GetAppointmentById(string id)
        {
            var appointment = await _readRepository.GetByIdAsync(id);
            return appointment;
        }

        public async Task<bool> UpdateAppointment(AppointmentDto appointmentDto)
        {

            var appointmentToUpdate = await _readRepository.GetByIdAsync(appointmentDto.Id);
            appointmentToUpdate.AppointmentDate = appointmentDto.AppointmentDate;
            appointmentToUpdate.ServiceId = appointmentDto.ServiceId;
            await _writeRepository.SaveAsync();
            return true;
        }
    }
}
