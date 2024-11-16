using OnlineAppointmentManagementSystem.Web.Models.Dto;

namespace OnlineAppointmentManagementSystem.Web.Services.Abstract
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
