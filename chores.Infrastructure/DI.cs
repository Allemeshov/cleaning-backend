using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace chores.Infrastructure;

public static class DI
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MainDb");

        services.AddDbContext<ChoreContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

        return services;
    }

    public static async Task MigrateDb(this IServiceProvider provider)
    {
        Console.WriteLine("Migrating Db");
        using var serviceScope = provider.CreateScope();
        await serviceScope.ServiceProvider.GetRequiredService<ChoreContext>().Database.MigrateAsync();
    }
}