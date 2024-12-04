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
            var role = User.FindFirstValue(ClaimTypes.Role);
            if (role!=null && role==StaticDetails.RoleAdmin)
                return RedirectToAction("AdminDashboard");
            
            else if (role!= null && role==StaticDetails.RoleUser)
                return RedirectToAction("UserDashboard");
            
            return RedirectToAction("Login","Auth");
        }
        [Authorize(Roles = StaticDetails.RoleAdmin)]
        public IActionResult AdminDashboard()
        {
            return View();
        }
        [Authorize(Roles = StaticDetails.RoleUser)]
        public IActionResult UserDashboard()
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
