using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Domain.Entities.Common;

namespace VehicleManagementSystem.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
