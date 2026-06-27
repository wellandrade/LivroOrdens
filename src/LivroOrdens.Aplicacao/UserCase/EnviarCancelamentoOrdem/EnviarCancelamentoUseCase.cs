using LivroOrdens.Aplicacao.Interfaces.Fix.Interface;
using LivroOrdens.Aplicacao.UserCase.CancelarOrdem;

namespace LivroOrdens.Aplicacao.UserCase.EnviarCancelamentoOrdem
{
    public class EnviarCancelamentoUseCase
    {
        private readonly IFixClienteService _fixClienteService;

        public EnviarCancelamentoUseCase(IFixClienteService fixClienteService)
        {
            _fixClienteService = fixClienteService;
        }

        public async Task<EnviarCancelamentoResponse> Executar(EnviarCancelamentoRequest cancelamentoRequest)
        {
            var cancelarRequest = new CancelarOrdemRequest
            {
                ClOrdId = cancelamentoRequest.CLOrId,
                OrigClOrdId = cancelamentoRequest.OrigClOrdId,
                Lado = cancelamentoRequest.Lado,
                Simbolo = cancelamentoRequest.Simbolo
            };

            var response = await _fixClienteService.CancelarOrdem(cancelarRequest);

            return new EnviarCancelamentoResponse
            {
                Sucesso = response.Sucesso,
                Mensagem = response.Mensagem
            };
        }

    }
}
