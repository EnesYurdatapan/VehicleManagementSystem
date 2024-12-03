using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Domain.Entities;

namespace VehicleManagementSystem.Application.Repositories
{
    public interface IVehicleReadRepository: IReadRepository<Vehicle>
    {
    }
}
