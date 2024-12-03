using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VehicleManagementSystem.Application.Abstraction.Services;
using VehicleManagementSystem.Application.Repositories;
using VehicleManagementSystem.Domain.Entities.Identity;
using VehicleManagementSystem.Persistance;
using VehicleManagementSystem.Persistance.Context;
using VehicleManagementSystem.Persistance.Repositories;
using VehicleManagementSystem.Persistance.Services;

namespace VehicleManagementSystem.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IVehicleReadRepository, VehicleReadRepository>();
            serviceCollection.AddScoped<IVehicleWriteRepository, VehicleWriteRepository>();
            serviceCollection.AddScoped<IVehicleUsageReadRepository, VehicleUsageReadRepository>();
            serviceCollection.AddScoped<IVehicleUsageWriteRepository, VehicleUsageWriteRepository>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IVehicleService, VehicleService>();
            serviceCollection.AddScoped<IVehicleUsageService, VehicleUsageService>();
            serviceCollection.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<VehicleDbContext>().AddDefaultTokenProviders();
            serviceCollection.AddDbContext<VehicleDbContext>(option =>
            {
                option.UseSqlServer(Configuration.ConnectionString);
            });
        }
    }
}
