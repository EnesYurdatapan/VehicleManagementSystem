using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Domain.Entities.Identity;

namespace VehicleManagementSystem.Application.Abstraction.Token
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser user, IEnumerable<string> roles);

    }
}
