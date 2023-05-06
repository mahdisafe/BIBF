using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Data.Identity;
using Infrastructure.Identity.Migrations;
using Microsoft.AspNetCore.Identity;

namespace API;

public class Program
{
    public static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var Services = scope.ServiceProvider;
            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = Services.GetRequiredService<StoreContext>();
                var identityContext = Services.GetRequiredService<AppIdentityDbContext>();
                var UserManager = Services.GetRequiredService<UserManager<AppUser>>();

                //context.Database.MigrateAsync();
                identityContext.Database.MigrateAsync();
                //StorecontextSeed.SeedAsync(context, loggerFactory);
                AppIdentityDbContextSeed.SeedUserAsync(UserManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex.Message, "An Error Occured During Migration");
            }
        }

        host.Run();
        return Task.CompletedTask;
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}