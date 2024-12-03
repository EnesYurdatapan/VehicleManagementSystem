using VehicleManagementSystem.Application.Repositories;
using VehicleManagementSystem.Domain.Entities;
using VehicleManagementSystem.Persistance.Context;

namespace VehicleManagementSystem.Persistance.Repositories
{
    public class VehicleReadRepository : ReadRepository<Vehicle>, IVehicleReadRepository
    {
        public VehicleReadRepository(VehicleDbContext vehicleDbContext) : base(vehicleDbContext)
        {
        }
    }
}
