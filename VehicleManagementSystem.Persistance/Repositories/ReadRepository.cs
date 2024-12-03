using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Application.Repositories;
using VehicleManagementSystem.Domain.Entities.Common;
using VehicleManagementSystem.Persistance.Context;

namespace VehicleManagementSystem.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {

        private readonly VehicleDbContext _vehicleDbContext;

        public ReadRepository(VehicleDbContext vehicleDbContext)
        {
            _vehicleDbContext = vehicleDbContext;
        }

        public DbSet<T> Table => _vehicleDbContext.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }


        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        =>await Table.FirstOrDefaultAsync(data=> data.Id == id);
        //{
        //    var query = Table.AsQueryable();
        //    if (!tracking)
        //        query = query.AsNoTracking();
        //    return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        //}
    }
}
