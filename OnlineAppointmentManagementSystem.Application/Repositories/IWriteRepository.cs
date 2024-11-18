using OnlineAppointmentManagementSystem.Domain.Entities.Common;

namespace OnlineAppointmentManagementSystem.Application.Repositories
{
    public interface IWriteRepository<T>:IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        bool Update(T entity);
        bool Delete(T entity);
        Task<bool> DeleteAsync(string id);
        bool DeleteRange(List<T> entities);
        Task<int> SaveAsync();
    }
}
