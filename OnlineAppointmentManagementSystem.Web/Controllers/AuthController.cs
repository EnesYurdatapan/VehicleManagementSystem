using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using OnlineAppointmentManagementSystem.Infrastructure.Utility;
using OnlineAppointmentManagementSystem.Application.Abstraction.Services;
using OnlineAppointmentManagementSystem.Application.Abstraction.Token;
using OnlineAppointmentManagementSystem.Application.DTOs;

namespace OnlineAppointmentManagementSystem.Web.Controllers
{
    
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;


        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=StaticDetails.RoleAdmin, Value=StaticDetails.RoleAdmin},
                new SelectListItem{Text=StaticDetails.RoleCustomer, Value=StaticDetails.RoleCustomer},
            };

            ViewBag.RoleList = roleList;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registerationRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }

            var responseDto = await _authService.RegisterAsync(registerationRequestDto);
            if (responseDto == null)
            {
                return BadRequest("Registration failed.");
            }

            if (string.IsNullOrEmpty(registerationRequestDto.Role))
            {
                registerationRequestDto.Role = StaticDetails.RoleCustomer;
            }

            var assignRole = await _authService.AssignRole(registerationRequestDto.Email, registerationRequestDto.Role);
            if (!assignRole)
            {
                return BadRequest("Role assignment failed.");
            }

            return Json(new { success = true, message = "Registration successful!" });
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
            var response = await _authService.LoginAsync(loginRequestDto);
            if (response != null)
            {
                await SignInUser(response);
                _tokenProvider.SetToken(response.Token);

                // Kullanıcının rolünü tespit et ve Session/Claim ile sakla
                var role = response.User.Role; // response içindeki role bilgisini alın

                HttpContext.Session.SetString("UserRole", role);

                // Role'e göre yönlendirme
                if (role == StaticDetails.RoleAdmin)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("GetAppointments", "Appointment");
                }
            }
            else
            {
                TempData["error"] = "Invalid username or password.";
                return View(loginRequestDto);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDto loginResponseDto)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(loginResponseDto.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));



            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
