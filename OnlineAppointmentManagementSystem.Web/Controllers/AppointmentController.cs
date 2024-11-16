using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineAppointmentManagementSystem.Web.Context;
using OnlineAppointmentManagementSystem.Web.Models;
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
		[Authorize]
		public IActionResult GetAppointments()
		{
			//var appointments = _context.Appointments.Include(a=>a.Service).Where(a => a.UserId == User.Claims.ToList()[1].Value).ToList();
			var appointments = _context.Appointments.Include(a => a.Service).ToList();
			return View(appointments);
		}
		public IActionResult AddAppointment()
		{
			var services = _context.Services.ToList();
			List<SelectListItem> serviceList = services.Select(service => new SelectListItem
			{
				Text = service.Name,
				Value = service.Id // veya farklı bir değer kullanabilirsiniz
			}).ToList();

			ViewBag.ServiceList = serviceList;
			return View();
		}
        [HttpPost]
        public IActionResult AddAppointment([FromBody] Appointment appointment)
        {
            try
            {
                appointment.AppUserId = User.Claims.ToList()[1].Value;
                appointment.Id = Guid.NewGuid().ToString();
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return Json(new { success = true, message = "Appointment added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult UpdateAppointment(string appointmentId)
		{
			var services = _context.Services.ToList();
			List<SelectListItem> serviceList = services.Select(service => new SelectListItem
			{
				Text = service.Name,
				Value = service.Id // veya farklı bir değer kullanabilirsiniz
			}).ToList();

			ViewBag.ServiceList = serviceList;

			var result = _context.Appointments.FirstOrDefault(a=>a.Id==appointmentId);
			return View(result);
		}
		[HttpPost]
		public IActionResult UpdateAppointment(Appointment appointment)
		{
			_context.Appointments.Update(appointment);
			_context.SaveChanges();
			return RedirectToAction(nameof(GetAppointments));
		}
		public IActionResult DeleteAppointment(string appointmentId)
		{
			var result = _context.Appointments.Find(appointmentId);
			return View(result);
		}
		[HttpPost]
		public IActionResult DeleteAppointment(Appointment appointment)
		{
			_context.Appointments.Remove(appointment);
			_context.SaveChanges();
			return RedirectToAction(nameof(GetAppointments));
		}
	}
}
