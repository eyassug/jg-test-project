using Jibble.HttpApi;
using Jibble.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Hangfire.Dashboard;
using System.Linq;
using System.Data.SqlClient;

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
            const string masterDb = "master";
            // Add helper to create an empty database as Hangfire requires an existing db
            var connectionString = configuration.GetConnectionString("Hangfire");
            var builder = new SqlConnectionStringBuilder(connectionString);

            // store 
            var hanfireDbName = builder.InitialCatalog;

            builder.InitialCatalog = masterDb;


            using (var cnx = new SqlConnection(builder.ConnectionString))
            {
                cnx.Open();

                using (var command = new SqlCommand(string.Format(
                    @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}') 
                                    create database [{0}];
                      ", hanfireDbName), cnx))
                {
                    command.ExecuteNonQuery();
                }
            }
            
            services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(connectionString);
            });
            services.AddHangfireServer();
        }

        public static bool DbExists(this EmployeeDbContext ctx)
        {
            try
            {
                ctx.Employees.Count();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }

    public class HangfireNoAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // Allow unauthorized access to Dashboard
            return true;
        }
    }
}
