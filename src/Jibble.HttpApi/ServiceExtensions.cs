using Jibble.Services;
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
    }
}
