using Core.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();

           using (var scope = host.Services.CreateScope())
           {
               /*
                * a factory interface that we can use to create instances of the ILogger type and register logging providers
                */
               var services = scope.ServiceProvider;

               var loggerFactory = services.GetRequiredService<ILoggerFactory>();

               try
               {
                   var context = services.GetRequiredService<StoreContext>();
                   await context.Database.MigrateAsync();
                   await StoreContextSeed.Seed(context, loggerFactory);
                   
                   var userManager = services.GetRequiredService<UserManager<AppUser>>();
                   var identityContext = services.GetRequiredService<AppIdentityDbContext>();

                   await identityContext.Database.MigrateAsync();
                   await AppIdebtityContextSeed.SeedUserAsync(userManager);
               }
               catch (Exception e)
               {
                   var logger = loggerFactory.CreateLogger<Program>();
                   logger.LogError(e , "An error occured during migration");
               }
           }
           
           host.Run();
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
