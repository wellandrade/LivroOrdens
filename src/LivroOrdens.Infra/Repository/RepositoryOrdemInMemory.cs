using LivroOrdens.Aplicacao.Interfaces.Repository;
using LivroOrdens.Dominio.Entidades;
using LivroOrdens.Dominio.Enum;
using System.Collections.Concurrent;

namespace LivroOrdens.Infra.Repository
{
    public class RepositoryOrdemInMemory : IRepositoryOrdem
    {
        private readonly ConcurrentDictionary<string, Ordem> _ordens = new();

        public Task Adicionar(Ordem orderm)
        {
            _ordens.TryAdd(orderm.CIOrdId, orderm);
            return Task.CompletedTask;
        }

        public Task<Ordem?> ObterOrdemPorCIOrId(string cIOrdId)
        {
            _ordens.TryGetValue(cIOrdId, out var ordem);
            return Task.FromResult(ordem);
        }
        public Task<bool> Remover(Ordem orderm)
        {
            var removeuOrdem = _ordens.TryRemove(orderm.CIOrdId, out var ordemRemovida);
            return Task.FromResult(removeuOrdem);
        }

        public Task<IReadOnlyCollection<Ordem>> ListarOrdensAtiva()
        {
            var ordensAtiva = _ordens.Values.Where(x => x.Status == EStatusOrdem.Nova).ToList();
            return Task.FromResult<IReadOnlyCollection<Ordem>>(ordensAtiva);
        }
    }
}
