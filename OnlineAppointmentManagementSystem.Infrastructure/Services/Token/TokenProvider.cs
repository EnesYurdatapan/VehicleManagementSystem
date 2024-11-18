using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentManagementSystem.Application.Abstraction.Token;
using OnlineAppointmentManagementSystem.Domain.Entities.Identity;
using OnlineAppointmentManagementSystem.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Infrastructure.Services.Token
{
    public class TokenProvider: ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly SignInManager<AppUser> _signInManager;

        public TokenProvider(IHttpContextAccessor contextAccessor, SignInManager<AppUser> signInManager)
        {
            _contextAccessor = contextAccessor;
            _signInManager = signInManager;
        }

        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(".AspNetCore.Cookies");
            //_signInManager.SignOutAsync().Wait();
        }

        public string? GetToken()
        {
            string? token = null;

            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticDetails.TokenCookie, out token);

            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(StaticDetails.TokenCookie, token);
        }
    }
}
