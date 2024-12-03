using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Application.Abstraction.Services;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Domain.Entities.Identity;
using VehicleManagementSystem.Infrastructure.Utility;

namespace VehicleManagementSystem.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IAuthService authService, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [Authorize(Roles = StaticDetails.RoleAdmin)]
        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            var usersToModel = new List<RegistrationRequestDto>();
            foreach (var user in users)
            {
                var userToModel = new RegistrationRequestDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault(),
                };
                usersToModel.Add(userToModel);
            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=StaticDetails.RoleAdmin, Value=StaticDetails.RoleAdmin},
                new SelectListItem{Text=StaticDetails.RoleCustomer, Value=StaticDetails.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View(usersToModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] RegistrationRequestDto registerationRequestDto)
        {
            var responseDto = await _authService.RegisterAsync(registerationRequestDto);
            bool assignRole;
            if (responseDto != null/* && responseDto.IsSuccess*/)
            {
                if (string.IsNullOrEmpty(registerationRequestDto.Role))
                {
                    registerationRequestDto.Role = StaticDetails.RoleCustomer;
                }
                assignRole = await _authService.AssignRole(registerationRequestDto.Email, registerationRequestDto.Role);
                if (assignRole != null && assignRole == true)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Appointment added successfully",
                        data = new
                        {
                            Id = registerationRequestDto.Id,
                            Name = registerationRequestDto.Name,
                            Email = registerationRequestDto.Email,
                            Phone = registerationRequestDto.PhoneNumber,
                            Role = registerationRequestDto.Role
                        }
                    });
                }
            }
            else
            {
                return Json(new { success = false, message = "Error" });

            }
            return Json(new { success = false, message = "Error" });

        }

        [HttpPost]
        public IActionResult DeleteUser(string userId)
        {
            var user = _userManager.Users.FirstOrDefault(u=>u.Id==userId);
            if (user != null)
            {
                _userManager.DeleteAsync(user);
                return Json(new { success = true, message = "user deleted successfully." });
            }
            return Json(new { success = false, message = "user not found." });
        }

        [HttpGet]
        public IActionResult GetUserById(string userId)
        {
            var user = _userManager.Users
                .FirstOrDefault(u => u.Id == userId);
            var role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            return Json(new
            {
                success = true,
                data = new
                {
                    UserId = user.Id,
                    Role = role
                }
            });
        }


        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser([FromBody] RegistrationRequestDto registrationRequestDto)
        {

            await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role);

            return Json(new { success = true, message = "User Role updated successfully." });
        }
    }
}
