using Jibble.HttpApi;
using Jibble.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Microsoft.Extensions.Configuration;
using AutoMapper;

namespace Jibble
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<Employees.IEmployeeRepository, Employees.EmployeeRepository>();
            services.AddScoped<ICSVImportService, CSVImportService>();
        }

        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
        }

        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(configuration.GetConnectionString("Default"));
            });
            services.AddHangfireServer();
        }
    }
}
