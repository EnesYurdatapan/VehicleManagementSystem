using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Metadata;
using VehicleManagementSystem.Domain.Entities.Identity;
using VehicleManagementSystem.Domain.Entities;

namespace VehicleManagementSystem.Persistance.Context
{
    public class VehicleDbContext : IdentityDbContext<AppUser>
    {
        public VehicleDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleUsage> VehicleUsages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
