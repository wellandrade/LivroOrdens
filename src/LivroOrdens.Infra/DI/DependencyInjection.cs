using LivroOrdens.Aplicacao.Interfaces.Repository;
using LivroOrdens.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace LivroOrdens.Infra.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            services.AddSingleton<IRepositoryOrdem, RepositoryOrdemInMemory>();
            return services;
        }
    }
}
