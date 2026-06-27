using LivroOrdens.Aplicacao.Interfaces.Fix.Interface;
using LivroOrdens.Fix.Cliente.Application;
using LivroOrdens.Fix.Cliente.HostedService;
using LivroOrdens.Fix.Cliente.Services;
using LivroOrdens.Fix.Cliente.Session;
using LivroOrdens.Fix.Server.Application;
using LivroOrdens.Fix.Server.HostService;
using Microsoft.Extensions.DependencyInjection;

namespace LivroOrdens.Fix.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFixClient(this IServiceCollection services)
        {
            services.AddSingleton<ClienteFixSession>();
            services.AddSingleton<ClienteFixApplication>();
            services.AddSingleton<IFixClienteService, FixClienteService>();
            services.AddHostedService<ClienteFixHostedService>();

            return services;
        }

        public static IServiceCollection AddFixServer(this IServiceCollection services)
        {
            services.AddSingleton<ServerFixApplication>();
            services.AddHostedService<ServerFixHostService>();
            return services;
        }
    }
}
