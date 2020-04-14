using Business.Interfaces;
using Business.Interfaces.Repository;
using Business.Interfaces.Service;
using Business.Notifications;
using Business.Services;
using Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IPaisService, PaisService>();
            services.AddScoped<IPaisRepository, PaisRepository>();
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();
            return services;
        }
    }
}
