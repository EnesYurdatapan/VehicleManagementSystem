using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Persistance
{
    public static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new ConfigurationManager();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OnlineAppointmentManagementSystem.Web"));
                configurationManager.AddJsonFile("appsettings.json");
                var x = configurationManager.GetConnectionString("DefaultConnection"); 
                return configurationManager.GetConnectionString("DefaultConnection");
            }
        }
    }
}
