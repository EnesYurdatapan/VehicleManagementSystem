using OnlineAppointmentManagementSystem.Application.DTOs;
using OnlineAppointmentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Application.Abstraction.Services
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> AddAppointment(AppointmentDto appointmentDto);
        Task<bool> DeleteAppointment(string appointmentId);
        Task<bool> UpdateAppointment(AppointmentDto appointmentDto);
        Task<bool> ChangeStatusOfAppointment(Appointment appointment);
        Task<List<Appointment>> GetAllAppointments();
        Task<Appointment> GetAppointmentById(string id);
    }
}
