using Microsoft.EntityFrameworkCore;
using OnlineAppointmentManagementSystem.Domain.Entities.Identity;
using OnlineAppointmentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace OnlineAppointmentManagementSystem.Persistance.Context
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

            modelBuilder.Entity<Appointment>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Service>()
                                .HasData(new Service() { Id = "1", Name="Egzoz Gazı Ölçümü" },
                                           new Service() { Id = "2", Name = "Far Ayarı" },
                                           new Service() { Id = "3", Name = "Fren Testi" });

        }
    }
}
