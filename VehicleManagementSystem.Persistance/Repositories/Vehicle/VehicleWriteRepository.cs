using VehicleManagementSystem.Application.Repositories;
using VehicleManagementSystem.Domain.Entities;
using VehicleManagementSystem.Persistance.Context;

namespace VehicleManagementSystem.Persistance.Repositories
{
    public class VehicleWriteRepository : WriteRepository<Vehicle>, IVehicleWriteRepository
    {
        public VehicleWriteRepository(VehicleDbContext vehicleDbContext) : base(vehicleDbContext)
        {
        }
    }
}
