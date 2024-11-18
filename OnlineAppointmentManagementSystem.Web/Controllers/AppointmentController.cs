using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineAppointmentManagementSystem.Application.Abstraction.Services;
using OnlineAppointmentManagementSystem.Application.DTOs;
using OnlineAppointmentManagementSystem.Domain.Entities;
using OnlineAppointmentManagementSystem.Infrastructure.Utility;

namespace OnlineAppointmentManagementSystem.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IServiceManager _serviceManager;

        public AppointmentController(IAppointmentService appointmentService, IServiceManager serviceManager)
        {
            _appointmentService = appointmentService;
            _serviceManager = serviceManager;
        }


        [Authorize(Roles = StaticDetails.RoleCustomer)]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments =  _appointmentService.GetAllAppointments().Result.Where(a => a.AppUserId == User.Claims.ToList()[1].Value).ToList();
            var services = await _serviceManager.GetAllServices();

            List<SelectListItem> serviceList = services.Select(service => new SelectListItem
            {
                Text = service.Name,
                Value = service.Id
            }).ToList();

            ViewBag.ServiceList = serviceList;

            return View(appointments);
        }
        [Authorize(Roles = StaticDetails.RoleAdmin)]
        public async Task<IActionResult> GetAppointmentsForAdmin()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            var services = await _serviceManager.GetAllServices();
            var statusList = new List<SelectListItem>()
            {
                new SelectListItem{Text=StaticDetails.Waiting, Value=StaticDetails.Waiting},
                new SelectListItem{Text=StaticDetails.Accepted, Value=StaticDetails.Accepted},
                new SelectListItem{Text=StaticDetails.Completed, Value=StaticDetails.Completed},
                new SelectListItem{Text=StaticDetails.Canceled, Value=StaticDetails.Canceled},
            };

            ViewBag.StatusList = statusList;

            return View(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                appointmentDto.AppUserId = User.Claims.ToList()[1].Value;
                var result = await _appointmentService.AddAppointment(appointmentDto);
                return Json(new
                {
                    success = true,
                    message = "Appointment added successfully",
                    data = new
                    {
                        Id = appointmentDto.Id,
                        AppointmentDate = appointmentDto.AppointmentDate.ToString("yyyy-MM-dd HH:mm"), 
                        ServiceName = appointmentDto.ServiceName 
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(string appointmentId)
        {
            var result = await _appointmentService.DeleteAppointment(appointmentId);
            if (result != false)
            {
                return Json(new { success = true, message = "Appointment deleted successfully." });
            }
            return Json(new { success = false, message = "Appointment not found." });
        }


        [HttpGet]
        public async Task<IActionResult> GetAppointmentById(string appointmentId)
        {
            var appointment = await _appointmentService.GetAppointmentById(appointmentId);
            var service = await _serviceManager.GetById(appointment.ServiceId);

            if (appointment == null)
            {
                return Json(new { success = false, message = "Appointment not found." });
            }

            return Json(new
            {
                success = true,
                data = new
                {
                    AppointmentId = appointment.Id,
                    AppointmentDate = appointment.AppointmentDate,
                    ServiceId = appointment.ServiceId,
                    ServiceName = service.Name
                }
            });
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            var result = await _appointmentService.UpdateAppointment(appointmentDto);

            if (result == false)
            {
                return Json(new { success = false, message = "Appointment not found." });
            }

            return Json(new { success = true, message = "Appointment updated successfully." });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatusOfAppointment([FromBody] Appointment appointment)
        {

            var result = await _appointmentService.ChangeStatusOfAppointment(appointment);
            if (result == false)
            {
                return Json(new { success = false, message = "Appointment not found." });
            }


            return Json(new { success = true, message = "Appointment updated successfully." });
        }


    }
}
