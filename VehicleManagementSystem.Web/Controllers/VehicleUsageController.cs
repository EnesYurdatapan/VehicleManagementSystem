using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleManagementSystem.Application.Abstraction.Services;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Infrastructure.Utility;

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
        [Authorize(Roles = StaticDetails.RoleUser)]
        public async Task<IActionResult> Index()
        {
            var vehicles = _vehicleService.GetAllVehicles();
            List<SelectListItem> vehicleList = vehicles.Data.Select(vehicle => new SelectListItem
            {
                Text = vehicle.Name + "-" + vehicle.Plate,
                Value = vehicle.Id
            }).ToList();

            ViewBag.VehicleList = vehicleList;

            return View();
        }
        [Authorize(Roles = StaticDetails.RoleAdmin)]
        public async Task<IActionResult> GetAllVehicleUsages()
        {
            var vehicleUsages = _vehicleUsageService.GetAllVehicleUsage();
            return View(vehicleUsages.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleUsage([FromBody] VehicleUsageDto vehicleUsageDto)
        {
            var result = await _vehicleUsageService.AddVehicleUsage(vehicleUsageDto);
            if (result.Success)
            {
                return Json(new
                {
                    success = result.Success,
                    message = result.Message,
                    data = new
                    {
                        Data = result.Data
                    }
                });
            }
            return Json(new
            {
                success = result.Success,
                message = result.Message
            });
        }
    }
}
