using LivroOrdens.Aplicacao.UserCase.CancelarOrdem;
using LivroOrdens.Aplicacao.UserCase.CriarOrdem;
using LivroOrdens.Aplicacao.UserCase.ObterLivrosOrdens;

namespace LivroOrdens.Aplicacao.Interfaces.Fix.Interface
{
    public interface IFixClienteService
    {
        Task<CriarOrdemResponse> EnviarNovaOrdem(CriarOrdermRequest request);
        Task<CancelarOrdemResponse> CancelarOrdem(CancelarOrdemRequest request);
        Task<ObterLivrosOrdensResponse> ObterLivrosOrdens(ObterLivrosOrdensRequest request);

        Task<ObterLivrosOrdensResponse> ObterLivroOrdens();
    }
}
