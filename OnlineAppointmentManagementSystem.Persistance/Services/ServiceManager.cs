using Microsoft.EntityFrameworkCore;
using OnlineAppointmentManagementSystem.Application.Abstraction.Services;
using OnlineAppointmentManagementSystem.Application.Repositories;
using OnlineAppointmentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Persistance.Services
{
    public class ServiceManager : IServiceManager
    {
        readonly IServiceReadRepository _repository;

        public ServiceManager(IServiceReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Service>> GetAllServices()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<Service> GetById(string id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
