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


        //[Authorize]
        //public IActionResult GetAppointments()
        //{
        //	//var appointments = _context.Appointments.Include(a=>a.Service).Where(a => a.UserId == User.Claims.ToList()[1].Value).ToList();
        //	var appointments = _context.Appointments.Include(a => a.Service).ToList();
        //	return View(appointments);
        //}
        //      public IActionResult AddAppointment()
        //{
        //	var services = _context.Services.ToList();
        //	List<SelectListItem> serviceList = services.Select(service => new SelectListItem
        //	{
        //		Text = service.Name,
        //		Value = service.Id // veya farklı bir değer kullanabilirsiniz
        //	}).ToList();

        //	ViewBag.ServiceList = serviceList;
        //	return View();
        //}
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
