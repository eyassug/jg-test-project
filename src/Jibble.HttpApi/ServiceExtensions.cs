using Jibble.HttpApi;
using Jibble.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Jibble
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<EmployeeDbContext>();
            services.AddScoped<Employees.IEmployeeRepository, Employees.EmployeeRepository>();
            services.AddScoped<ICSVImportService, CSVImportService>();
        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
        }
    }
}
