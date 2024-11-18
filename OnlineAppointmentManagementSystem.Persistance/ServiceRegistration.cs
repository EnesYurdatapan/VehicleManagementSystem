using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineAppointmentManagementSystem.Application.Abstraction.Services;
using OnlineAppointmentManagementSystem.Application.Repositories;
using OnlineAppointmentManagementSystem.Domain.Entities.Identity;
using OnlineAppointmentManagementSystem.Persistance.Context;
using OnlineAppointmentManagementSystem.Persistance.Repositories;
using OnlineAppointmentManagementSystem.Persistance.Services;

namespace OnlineAppointmentManagementSystem.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IAppointmentService, AppointmentService>();
            serviceCollection.AddScoped<IServiceManager, ServiceManager>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IAppointmentReadRepository, AppointmentReadRepository>();
            serviceCollection.AddScoped<IAppointmentWriteRepository, AppointmentWriteRepository>();
            serviceCollection.AddScoped<IServiceReadRepository, ServiceReadRepository>();
            serviceCollection.AddScoped<IServiceWriteRepository, ServiceWriteRepository>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppointmentDbContext>().AddDefaultTokenProviders();
            serviceCollection.AddDbContext<AppointmentDbContext>(option =>
            {
                option.UseSqlServer(Configuration.ConnectionString);
            });
        }
    }
}
