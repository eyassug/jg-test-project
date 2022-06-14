using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibble
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<EmployeeDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<EmployeeDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDb");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<EmployeeDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                }
            });
        }
    }
}
