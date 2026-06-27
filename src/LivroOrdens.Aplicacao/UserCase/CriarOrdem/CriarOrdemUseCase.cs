using LivroOrdens.Aplicacao.Interfaces.Repository;
using LivroOrdens.Dominio.Entidades;
using LivroOrdens.Dominio.Expcetions;

namespace LivroOrdens.Aplicacao.UserCase.CriarOrdem
{
    public class CriarOrdemUseCase
    {
        private readonly IRepositoryOrdem _repositoryOrdem;

        public CriarOrdemUseCase(IRepositoryOrdem repositoryOrdem)
        {
            _repositoryOrdem = repositoryOrdem;
        }

        public async Task<CriarOrdemResponse> Executar(CriarOrdermRequest criarOrdermRequest)
        {
            try
            {
                var ordemExistente = await _repositoryOrdem.ObterOrdemPorCIOrId(criarOrdermRequest.CIOrdId);

                if (ordemExistente is not null)
                {
                    return new CriarOrdemResponse
                    {
                        Sucesso = false,
                        Mensagem = "Já existe uma ordem com o mesmo ID."
                    };
                }

                var ordem = new Ordem(criarOrdermRequest.Simbolo, criarOrdermRequest.Lado,
                    criarOrdermRequest.Quantidade, criarOrdermRequest.Preco, criarOrdermRequest.CIOrdId);

                await _repositoryOrdem.Adicionar(ordem);

                return new CriarOrdemResponse
                {
                    Sucesso = true,
                    Mensagem = "Ordem criada com sucesso."
                };
            }
            catch (DominioException ex)
            {
                return new CriarOrdemResponse
                {
                    Sucesso = false,
                    Mensagem = ex.Message
                };
            }
        }
    }
}
