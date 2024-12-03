
using VehicleManagementSystem.Application.Repositories;
using VehicleManagementSystem.Domain.Entities;
using VehicleManagementSystem.Persistance.Context;

namespace VehicleManagementSystem.Persistance.Repositories
{
    public class VehicleUsageWriteRepository : WriteRepository<VehicleUsage>, IVehicleUsageWriteRepository
    {
        public VehicleUsageWriteRepository(VehicleDbContext vehicleDbContext) : base(vehicleDbContext)
        {
        }
    }
}
