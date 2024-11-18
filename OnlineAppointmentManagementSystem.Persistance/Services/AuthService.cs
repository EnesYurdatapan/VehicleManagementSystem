using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentManagementSystem.Application.Abstraction.Services;
using OnlineAppointmentManagementSystem.Application.Abstraction.Token;
using OnlineAppointmentManagementSystem.Application.DTOs;
using OnlineAppointmentManagementSystem.Domain.Entities.Identity;
using OnlineAppointmentManagementSystem.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Persistance.Services
{
    public class AuthService:IAuthService
    {
        private readonly AppointmentDbContext _appointmentDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppointmentDbContext appointmentDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _appointmentDbContext = appointmentDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _appointmentDbContext.AppUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
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

                    return true;
                }
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, roleToDelete);
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return true;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = _appointmentDbContext.AppUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

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

            return loginResponseDto;
        }

        public async Task<string> RegisterAsync(RegistrationRequestDto registerationRequestDto)
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
            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _appointmentDbContext.AppUsers.AsNoTracking().First(u => u.UserName == registerationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber,
                        Id = userToReturn.Id
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }
    }
}
