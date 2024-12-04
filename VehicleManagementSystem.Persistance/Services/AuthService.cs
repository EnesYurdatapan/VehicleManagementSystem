using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Application.Abstraction.Services;
using VehicleManagementSystem.Application.Abstraction.Token;
using VehicleManagementSystem.Application.Constants;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Application.Results;
using VehicleManagementSystem.Domain.Entities.Identity;
using VehicleManagementSystem.Persistance.Context;

namespace VehicleManagementSystem.Persistance.Services
{
    public class AuthService : IAuthService
    {
        private readonly VehicleDbContext _vehicleDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(VehicleDbContext vehicleDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _vehicleDbContext = vehicleDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<IResult> AssignRole(string email, string roleName)
        {
            var user = await _vehicleDbContext.AppUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                var roleToDelete = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
                if (roleToDelete == null)
                {
                    if (user != null)
                    {
                        if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                        {
                            await _roleManager.CreateAsync(new IdentityRole(roleName));
                        }

                        var result = await _userManager.AddToRoleAsync(user, roleName);

                        return new SuccessResult(Messages.RoleSuccessfullyAddedMessage);
                    }
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, roleToDelete);
                    await _userManager.AddToRoleAsync(user, roleName);
                    return new SuccessResult(Messages.RoleCouldntAddedMessage);
                }
            }
            return new ErrorResult(Messages.UserNotFoundForRoleMessage);
        }

        public async Task<IDataResult<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            AppUser user = await _vehicleDbContext.AppUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
                return new ErrorDataResult<LoginResponseDto>(new LoginResponseDto() { User = null, Token = "" },Messages.UserDatasNotValidMessage);

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);
            if (roles != null && token != null)
            {
                UserDto userDto = new()
                {
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Id = user.Id,
                    Role = roles.First(),
                };

                LoginResponseDto loginResponseDto = new LoginResponseDto()
                {
                    User = userDto,
                    Token = token
                };
                return new SuccessDataResult<LoginResponseDto>(loginResponseDto, Messages.SuccessfullyLoggedInMessage);
            }
            return new ErrorDataResult<LoginResponseDto>(Messages.UserRoleAndTokenErrorMessage);
        }

        public async Task<IResult> RegisterAsync(RegistrationRequestDto registerationRequestDto)
        {
            AppUser user = new()
            {
                //Id = registerationRequestDto.Id,
                UserName = registerationRequestDto.Email,
                Email = registerationRequestDto.Email,
                NormalizedEmail = registerationRequestDto.Email.ToUpper(),
                Name = registerationRequestDto.Name,
                PhoneNumber = registerationRequestDto.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registerationRequestDto.Password);
            await AssignRole(registerationRequestDto.Email, registerationRequestDto.Role);
            if (result.Succeeded)
                return new SuccessResult(Messages.UserSuccessfullyCreatedMessage);

            return new ErrorResult(result.Errors.First().Description);

        }

    }
}
