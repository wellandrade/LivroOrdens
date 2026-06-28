using LivroOrdens.Aplicacao.Shared;

namespace LivroOrdens.Aplicacao.UserCase.ObterLivrosOrdens
{
    public class ObterLivrosOrdensResponse : BaseResponse
    {
        public List<OrdemLivroResponse> LivrosOrdens { get; set; } = [];
    }
}
