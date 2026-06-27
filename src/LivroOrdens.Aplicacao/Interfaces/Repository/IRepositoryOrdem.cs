using LivroOrdens.Dominio.Entidades;

namespace LivroOrdens.Aplicacao.Interfaces.Repository
{
    public interface IRepositoryOrdem
    {
        Task Adicionar(Ordem orderm);
        Task<bool> Remover(Ordem orderm);
        Task<Ordem?> ObterOrdemPorCIOrId(string cIOrdId);
        Task<IReadOnlyCollection<Ordem>> ListarOrdensAtiva();
    }
}
