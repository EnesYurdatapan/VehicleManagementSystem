using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineAppointmentManagementSystem.Web.Models;
using OnlineAppointmentManagementSystem.Web.Utility;
using System.Diagnostics;
using System.Security.Claims;

namespace OnlineAppointmentManagementSystem.Web.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            //var role = HttpContext.Session.GetString("UserRole");

            if (role == StaticDetails.RoleAdmin)
            {
                return View("AdminDashboard");
            }
            else if (role == StaticDetails.RoleCustomer)
            {
                return RedirectToAction("GetAppointments", "Appointment");
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }
        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
