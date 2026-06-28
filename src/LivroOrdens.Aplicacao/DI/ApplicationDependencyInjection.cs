using LivroOrdens.Aplicacao.UserCase.CancelarOrdem;
using LivroOrdens.Aplicacao.UserCase.CriarOrdem;
using LivroOrdens.Aplicacao.UserCase.EnviarCancelamentoOrdem;
using LivroOrdens.Aplicacao.UserCase.EnviarOrdem;
using LivroOrdens.Aplicacao.UserCase.ObterLivrosOrdens;
using Microsoft.Extensions.DependencyInjection;

namespace LivroOrdens.Aplicacao.DI
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<CriarOrdemUseCase>();
            services.AddSingleton<CancelarOrdemUseCase>();
            services.AddSingleton<ObterLivrosOrdensUseCase>();
            services.AddSingleton<EnviarOrdemUseCase>();
            services.AddSingleton<EnviarCancelamentoUseCase>();

            return services;
        }

        public static IServiceCollection AddServerApplication(this IServiceCollection services)
        {
            services.AddSingleton<CriarOrdemUseCase>();
            services.AddSingleton<CancelarOrdemUseCase>();
            services.AddSingleton<ObterLivrosOrdensUseCase>();

            return services;
        }
    }
}
