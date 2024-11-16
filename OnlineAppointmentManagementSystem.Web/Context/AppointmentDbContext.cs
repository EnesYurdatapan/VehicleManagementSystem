using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentManagementSystem.Web.Models;

namespace OnlineAppointmentManagementSystem.Web.Context
{
    public class AppointmentDbContext : IdentityDbContext<AppUser>
    {
        public AppointmentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>().Property(a=>a.Id).ValueGeneratedOnAdd();

        }
    }
}
