using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Extensions;

public static class AddDatabaseMigrationExtensions
{

    public static IServiceCollection AddDatabaseMigration<TContext>(this IServiceCollection services, Action<TContext, IServiceProvider> seeder, int? retry = 0) where TContext : DbContext
    {
        int retryForAvailability = retry.Value;

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
            var context = serviceProvider.GetService<TContext>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                InvokeSeeder(seeder, context, serviceProvider);

                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    AddDatabaseMigration(services, seeder, retryForAvailability);
                }
            }
        }

        return services;
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, 
        TContext context, IServiceProvider services)
        where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }

}
