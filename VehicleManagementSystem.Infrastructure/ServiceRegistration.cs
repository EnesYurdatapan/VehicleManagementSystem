using Microsoft.Extensions.DependencyInjection;
using System;
using VehicleManagementSystem.Application.Abstraction.Token;
using VehicleManagementSystem.Infrastructure.Services.Token;

namespace VehicleManagementSystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            serviceCollection.AddScoped<ITokenProvider, TokenProvider>();


        }
    }
}
