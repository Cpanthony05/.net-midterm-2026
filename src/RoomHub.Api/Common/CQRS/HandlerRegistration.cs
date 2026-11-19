using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace RoomHub.Api.Common.CQRS;

public static class HandlerRegistration
{
    public static IServiceCollection AddHandlers(this IServiceCollection services, Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false })
            .SelectMany(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsGenericType &&
                    (interfaceType.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                     interfaceType.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                .Select(interfaceType => new { HandlerType = type, InterfaceType = interfaceType }));

        foreach (var handler in handlerTypes)
            services.AddTransient(handler.InterfaceType, handler.HandlerType);

        return services;
    }
}

