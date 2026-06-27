using LivroOrdens.Aplicacao.Interfaces.Repository;
using LivroOrdens.Dominio.Expcetions;

namespace LivroOrdens.Aplicacao.UserCase.CancelarOrdem
{
    public class CancelarOrdemUseCase
    {
        private readonly IRepositoryOrdem _repositoryOrdem;

        public CancelarOrdemUseCase(IRepositoryOrdem repositoryOrdem)
        {
            _repositoryOrdem = repositoryOrdem;
        }


        public async Task<CancelarOrdemResponse> Executar(CancelarOrdemRequest cancelarOrdemRequest)
        {
            try
            {
                var ordem = await _repositoryOrdem.ObterOrdemPorCIOrId(cancelarOrdemRequest.ClOrdId);
                if (ordem is null)
                {
                    return new CancelarOrdemResponse
                    {
                        Sucesso = false,
                        Mensagem = "Não existe uma ordem com o ID fornecido."
                    };
                }

                var removeuOrdem = await _repositoryOrdem.Remover(ordem);
                if (!removeuOrdem)
                {
                    return new CancelarOrdemResponse
                    {
                        Sucesso = false,
                        Mensagem = "Não foi possível cancelar a ordem."
                    };
                }

                return new CancelarOrdemResponse
                {
                    Sucesso = true,
                    Mensagem = "Ordem cancelada com sucesso."
                };
            }
            catch (DominioException ex)
            {
                return new CancelarOrdemResponse
                {
                    Sucesso = false,
                    Mensagem = ex.Message
                };
            }
        }
    }
}
