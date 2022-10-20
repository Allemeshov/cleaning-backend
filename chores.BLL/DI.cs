using System.Reflection;
using chores.BLL.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace chores.BLL;

public static class DI
{
    public static IServiceCollection AddBLL(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<ISessionHolder, SessionHolder>();

        return services;
    }
}