using OnlineAppointmentManagementSystem.Web.Models;

namespace OnlineAppointmentManagementSystem.Web.Services.Abstract
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser user, IEnumerable<string> roles);

    }
}
