using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using VehicleManagementSystem.Application.DTOs;
using VehicleManagementSystem.Infrastructure;
using VehicleManagementSystem.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

var x = builder.Configuration.GetSection("JwtOptions");
var y = builder.Configuration["JwtOptions:Audience"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearerScheme";
    options.DefaultChallengeScheme = "JwtBearerScheme";
})
.AddJwtBearer("JwtBearerScheme", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Secret"])),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtOptions:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});



// Yetkilendirme
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            // Loglama veya response dönme
            await context.Response.WriteAsync($"Error: {error.Error.Message}");
        }
    });
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
