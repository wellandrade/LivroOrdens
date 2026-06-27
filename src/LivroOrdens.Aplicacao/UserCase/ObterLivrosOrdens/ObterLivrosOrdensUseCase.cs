using LivroOrdens.Aplicacao.Interfaces.Repository;

namespace LivroOrdens.Aplicacao.UserCase.ObterLivrosOrdens
{
    public class ObterLivrosOrdensUseCase
    {
        private readonly IRepositoryOrdem _repositoryOrdem;

        public ObterLivrosOrdensUseCase(IRepositoryOrdem repositoryOrdem)
        {
            _repositoryOrdem = repositoryOrdem;
        }

        public async Task<ObterLivrosOrdensResponse> Executar()
        {
            var listaDeOrdens = await _repositoryOrdem.ListarOrdensAtiva();

            var ordensOrdenadas = listaDeOrdens.OrderBy(x => x.Simbolo)
                .ThenBy(x => x.Lado)
                .ThenBy(x => x.Preco)
                .ThenBy(x => x.CriadEm)
                .Select(x => new OrdemLivroResponse
                {
                    ClOrIdid = x.CIOrdId,
                    Simbolo = x.Simbolo,
                    Lado = x.Lado,
                    Quantidade = x.Quantidade,
                    Preco = x.Preco,
                }).ToList();

            return new ObterLivrosOrdensResponse
            {
                Sucesso = true,
                Mensagem = "Livros de ordens obtidos com sucesso.",
                LivrosOrdens = ordensOrdenadas
            };
        }
    }
}
