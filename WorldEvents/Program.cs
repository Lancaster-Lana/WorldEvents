using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using WorldEvents.DBModel;
using System;

namespace WorldEvents
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            //Seed DB. 
            //NOTE: if DB is Microsoft.EntityFrameworkCore.DbContext, not System.Data.Entity.DbContext 
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SatteliteDbContext>();
                    SatteliteDBInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
