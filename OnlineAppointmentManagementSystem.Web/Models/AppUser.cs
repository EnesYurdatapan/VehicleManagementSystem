using Microsoft.AspNetCore.Identity;

namespace OnlineAppointmentManagementSystem.Web.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
