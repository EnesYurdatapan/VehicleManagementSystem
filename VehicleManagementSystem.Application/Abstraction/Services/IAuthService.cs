using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Application.DTOs;

namespace VehicleManagementSystem.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
