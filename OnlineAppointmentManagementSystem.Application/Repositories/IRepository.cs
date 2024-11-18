using Microsoft.EntityFrameworkCore;
using OnlineAppointmentManagementSystem.Domain.Entities.Common;

namespace OnlineAppointmentManagementSystem.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
