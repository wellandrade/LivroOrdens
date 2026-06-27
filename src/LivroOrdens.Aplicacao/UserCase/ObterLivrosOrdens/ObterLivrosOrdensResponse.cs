using LivroOrdens.Aplicacao.Shared;

namespace LivroOrdens.Aplicacao.UserCase.ObterLivrosOrdens
{
    public class ObterLivrosOrdensResponse : BaseResponse
    {
        public IReadOnlyCollection<OrdemLivroResponse> LivrosOrdens { get; set; } = [];
    }
}
