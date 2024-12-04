using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using VehicleManagementSystem.Application.Abstraction.Services;
using VehicleManagementSystem.Application.Abstraction.Token;
using VehicleManagementSystem.Infrastructure.Utility;
using VehicleManagementSystem.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using VehicleManagementSystem.Domain.Entities.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VehicleManagementSystem.Infrastructure.Services.Token;
using Newtonsoft.Json.Linq;
using System.Data;

namespace VehicleManagementSystem.Web.Controllers
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
                new SelectListItem{Text=StaticDetails.RoleUser, Value=StaticDetails.RoleUser},
            };

            ViewBag.RoleList = roleList;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registerationRequestDto)
        {
            if (string.IsNullOrEmpty(registerationRequestDto.Role))
                registerationRequestDto.Role = StaticDetails.RoleUser;

            var result = await _authService.RegisterAsync(registerationRequestDto);

            return Json(new { success = result.Success, message = result.Message });
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await _authService.LoginAsync(loginRequestDto);
            if (result.Success)
            {
                var role = result.Data.User.Role; // response içindeki role bilgisini alın
                await SignInUser(result.Data);
                return Json(new
                {
                    success = result.Success,
                    message = result.Message,
                    data = new
                    {
                        Role = role,
                        Token = result.Data.Token
                    }
                });
            }
            else
            {
                return Json(new
                {
                    success = result.Success,
                    message = result.Message
                });
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
