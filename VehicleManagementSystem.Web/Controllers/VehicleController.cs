using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Application.Abstraction.Services;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Domain.Entities;
using VehicleManagementSystem.Infrastructure.Utility;

namespace VehicleManagementSystem.Web.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        [Authorize(Roles = StaticDetails.RoleAdmin)]
        public async Task<IActionResult> Index()
        {
            var vehicles = _vehicleService.GetAllVehicles();
            if (vehicles.Data== null)
            {
                return View();
            }
            return View(vehicles.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleDto vehicleDto)
        {
            var result = await _vehicleService.AddVehicle(vehicleDto);
            if (result.Success)
            {
                return Json(new
                {
                    success = result.Success,
                    message = result.Message,
                    data = new
                    {
                        Id = result.Data.Id,
                        Name = result.Data.Name,
                        Plate = result.Data.Plate
                    }
                });
            }
            return Json(new
            {
                success = result.Success,
                message = result.Message
            });

        }

        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(string vehicleId)
        {
            var result = await _vehicleService.DeleteVehicle(vehicleId);
            return Json(new { success = result.Success, message = result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleById(string vehicleId)
        {
            var result = await _vehicleService.GetVehicleById(vehicleId);

            if (result.Success)
            {
                return Json(new
                {
                    success = result.Success,
                    data = new
                    {
                        Id = result.Data.Id,
                        Name = result.Data.Name,
                        Plate = result.Data.Plate
                    }
                });
            }
            return Json(new { success = result.Success, message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVehicle([FromBody] VehicleDto vehicleDto)
        {
            var result = await _vehicleService.UpdateVehicle(vehicleDto);
            return Json(new { success = result.Success, message = result.Message });
        }
    }
}
