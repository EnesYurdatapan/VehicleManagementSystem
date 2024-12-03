using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleManagementSystem.Application.Abstraction.Services;
using VehicleManagementSystem.Application.DTOs;

namespace VehicleManagementSystem.Web.Controllers
{
    public class VehicleUsageController : Controller
    {
        private readonly IVehicleUsageService _vehicleUsageService;
        private readonly IVehicleService _vehicleService;

        public VehicleUsageController(IVehicleUsageService vehicleUsageService, IVehicleService vehicleService)
        {
            _vehicleUsageService = vehicleUsageService;
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = _vehicleService.GetAllVehicles();
            List<SelectListItem> vehicleList = vehicles.Select(vehicle => new SelectListItem
            {
                Text = vehicle.Name + "-" + vehicle.Plate,
                Value = vehicle.Id
            }).ToList();

            ViewBag.VehicleList = vehicleList;

            return View();
        }

        public async Task<IActionResult> GetAllVehicleUsages()
        {
            var vehicleUsages = _vehicleUsageService.GetAllVehicleUsage();
            return View(vehicleUsages);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleUsage([FromBody] VehicleUsageDto vehicleUsageDto)
        {
            try
            {
                var result = await _vehicleUsageService.AddVehicleUsage(vehicleUsageDto);
                return Json(new
                {
                    success = true,
                    message = "Vehicle Usage Successfully Saved",
                    data = new
                    {
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
