using OnlineAppointmentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Application.Abstraction.Services
{
    public interface IServiceManager
    {
        Task<List<Service>> GetAllServices();
        Task<Service> GetById(string id);
    }
}
