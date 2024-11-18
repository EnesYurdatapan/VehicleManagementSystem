using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineAppointmentManagementSystem.Web.Context;
using OnlineAppointmentManagementSystem.Web.Models;
using OnlineAppointmentManagementSystem.Web.Models.Dto;
using OnlineAppointmentManagementSystem.Web.Utility;

namespace OnlineAppointmentManagementSystem.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentDbContext _context;

        public AppointmentController(AppointmentDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = StaticDetails.RoleCustomer)]
        public IActionResult GetAppointments()
        {
            var appointments = _context.Appointments.Include(a => a.Service).Where(a => a.AppUserId == User.Claims.ToList()[1].Value).ToList();
            var services = _context.Services.ToList();

            List<SelectListItem> serviceList = services.Select(service => new SelectListItem
            {
                Text = service.Name,
                Value = service.Id
            }).ToList();

            ViewBag.ServiceList = serviceList;

            return View(appointments);
        }
        [Authorize(Roles = StaticDetails.RoleAdmin)]
        public IActionResult GetAppointmentsForAdmin()
        {
            var appointments = _context.Appointments.Include(a => a.Service).Include(a=>a.AppUser).ToList();
            var services = _context.Services.ToList();

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
        public IActionResult AddAppointment([FromBody] Appointment appointment)
        {
            try
            {
                // Kullanıcı ID'sini ayarla (sisteme giriş yapan kullanıcıdan alınır)
                appointment.AppUserId = User.Claims.ToList()[1].Value;

                // Randevu ID'sini oluştur
                appointment.Id = Guid.NewGuid().ToString();

                // Veritabanına kaydet
                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                // İlişkili veriyi al (Service Name gibi)
                var service = _context.Services.FirstOrDefault(s => s.Id == appointment.ServiceId);

                // JSON yanıtı döndür
                return Json(new
                {
                    success = true,
                    message = "Appointment added successfully",
                    data = new
                    {
                        Id = appointment.Id,
                        AppointmentDate = appointment.AppointmentDate.ToString("yyyy-MM-dd HH:mm"), // Tarih formatı
                        ServiceName = service?.Name // Servis adı
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        
        [HttpPost]
        public IActionResult DeleteAppointment(string appointmentId)
        {
            var appointment = _context.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
                return Json(new { success = true, message = "Appointment deleted successfully." });
            }
            return Json(new { success = false, message = "Appointment not found." });
        }


        [HttpGet]
        public IActionResult GetAppointmentById(string appointmentId)
        {
            var appointment = _context.Appointments
                .Include(a => a.Service)
                .FirstOrDefault(a => a.Id == appointmentId);

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
                    ServiceName = appointment.Service.Name
                }
            });
        }


        [HttpPost]
        public IActionResult UpdateAppointment([FromBody]Appointment appointment)
        {
            var existingAppointment = _context.Appointments.FirstOrDefault(a => a.Id == appointment.Id);

            if (existingAppointment == null)
            {
                return Json(new { success = false, message = "Appointment not found." });
            }

            existingAppointment.ServiceId = appointment.ServiceId;
            existingAppointment.AppointmentDate = appointment.AppointmentDate;

            _context.SaveChanges();
            return Json(new { success = true, message = "Appointment updated successfully." });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatusOfAppointment([FromBody] Appointment appointment)
        {

            var existingAppointment = _context.Appointments.FirstOrDefault(a => a.Id == appointment.Id);

            if (existingAppointment == null)
            {
                return Json(new { success = false, message = "Appointment not found." });
            }

            existingAppointment.Status = appointment.Status;
            _context.SaveChangesAsync();
            return Json(new { success = true, message = "Appointment updated successfully." });
        }


    }
}
