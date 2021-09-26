using ADO_DotNet.DAL.dbConfig;
using ADO_DotNet.DAL.Implementations;
using ADO_DotNet.DAL.Interfaces;
using ADO_DotNet.Services.Configuration;
using ADO_DotNet.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_DotNet.Services.Configuration
{
    public static class ConfigurationDependencies
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //database
            services.AddSingleton<DatabaseContext>();

            // Repositories
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            //services
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}
