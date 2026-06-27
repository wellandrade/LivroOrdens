using LivroOrdens.Aplicacao.Interfaces.Fix.Interface;
using LivroOrdens.Aplicacao.UserCase.CriarOrdem;

namespace LivroOrdens.Aplicacao.UserCase.EnviarOrdem
{
    public class EnviarOrdemUseCase
    {
        private readonly IFixClienteService _fixClienteService;

        public EnviarOrdemUseCase(IFixClienteService fixClienteService)
        {
            _fixClienteService = fixClienteService;
        }

        public async Task<EnviarOrdemResponse> Executar(EnviarOrdermRequest enviarOrdermRequest)
        {
            var criarOrdemRequest = new CriarOrdermRequest
            {
                CIOrdId = enviarOrdermRequest.CIOrdId,
                Simbolo = enviarOrdermRequest.Simbolo,
                Lado = enviarOrdermRequest.Lado,
                Quantidade = enviarOrdermRequest.Quantidade,
                Preco = enviarOrdermRequest.Preco,
            };

            var response = await _fixClienteService.EnviarNovaOrdem(criarOrdemRequest);

            return new EnviarOrdemResponse
            {
                Sucesso = response.Sucesso,
                Mensagem = response.Mensagem
            };
        }

    }
}
