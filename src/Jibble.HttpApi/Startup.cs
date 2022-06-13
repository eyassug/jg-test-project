using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace Jibble.HttpApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(new HttpResponseExceptionFilter());
                options.Filters.Add(new OperationCancelledExceptionFilter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jibble.HttpApi", Version = "v1" });
            });

            // Configure DbContext
            services.AddDbContext<EmployeeDbContext>(
                    options => options.UseSqlServer(Configuration.GetConnectionString("Default"),
                    opts => opts.MigrationsAssembly("Jibble.EntityFrameworkCore")));

            services.ConfigureRepositories();
            services.ConfigureMediatR();
            services.ConfigureAutomapper();
            // Hangfire
            services.ConfigureHangfire(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jibble.HttpApi v1"));
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
                context.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                IsReadOnlyFunc = (DashboardContext context) => true
            });

        }
    }
}
