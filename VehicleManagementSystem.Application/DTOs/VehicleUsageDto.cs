
namespace VehicleManagementSystem.Application.DTOs
{
    public class VehicleUsageDto
    {
        public string Id { get; set; }
        public string VehicleId { get; set; }
        public double ActiveHours { get; set; }
        public double MaintenanceHours { get; set; }
        public double IdleHours { get; set; }
    }
}
