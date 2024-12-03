using VehicleManagementSystem.Application.Repositories;
using VehicleManagementSystem.Domain.Entities;
using VehicleManagementSystem.Persistance.Context;

namespace VehicleManagementSystem.Persistance.Repositories
{
    public class VehicleUsageReadRepository : ReadRepository<VehicleUsage>, IVehicleUsageReadRepository
    {
        public VehicleUsageReadRepository(VehicleDbContext vehicleDbContext) : base(vehicleDbContext)
        {
        }
    }
}
