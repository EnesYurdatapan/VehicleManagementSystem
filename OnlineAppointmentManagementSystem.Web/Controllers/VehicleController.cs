using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineAppointmentManagementSystem.Application.Abstraction.Services;
using OnlineAppointmentManagementSystem.Application.DTOs;
using OnlineAppointmentManagementSystem.Persistance.Services;

namespace OnlineAppointmentManagementSystem.Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
         
            var vehicles = _vehicleService.GetAllVehicles();
            if (vehicles == null)
            {
                return View();
            }
            return View(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleDto vehicleDto)
        {
            try
            {
                var result = await _vehicleService.AddVehicle(vehicleDto);
                return Json(new
                {
                    success = true,
                    message = "Vehicle added successfully",
                    data = new
                    {
                        Id = vehicleDto.Id,
                        Name = vehicleDto.Name,
                        Plate = vehicleDto.Plate
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(string vehicleId)
        {
            var result = await _vehicleService.DeleteVehicle(vehicleId);
            if (result != false)
            {
                return Json(new { success = true, message = "Vehicle deleted successfully." });
            }
            return Json(new { success = false, message = "Vehicle not found." });
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleById(string vehicleId)
        {
            var vehicle = await _vehicleService.GetVehicleById(vehicleId);

            if (vehicle == null)
            {
                return Json(new { success = false, message = "Vehicle not found." });
            }

            return Json(new
            {
                success = true,
                data = new
                {
                 Id= vehicle.Id,
                 Name = vehicle.Name,
                 Plate = vehicle.Plate
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVehicle([FromBody] VehicleDto vehicleDto)
        {
            var result = await _vehicleService.UpdateVehicle(vehicleDto);

            if (result == false)
            {
                return Json(new { success = false, message = "Vehicle not found." });
            }

            return Json(new { success = true, message = "Vehicle updated successfully." });
        }
    }
}
