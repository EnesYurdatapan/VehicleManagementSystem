using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Application.Results;

namespace VehicleManagementSystem.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<IDataResult<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto);
        Task<IResult> AssignRole(string email, string roleName);
    }
}
