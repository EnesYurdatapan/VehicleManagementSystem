using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using VehicleManagementSystem.Infrastructure.Utility;
using VehicleManagementSystem.Web.Models;

namespace VehicleManagementSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            var username = User.Identity.Name;
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
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
