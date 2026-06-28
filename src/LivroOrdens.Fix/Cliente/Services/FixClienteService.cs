using LivroOrdens.Aplicacao.Interfaces.Fix.Interface;
using LivroOrdens.Aplicacao.UserCase.CancelarOrdem;
using LivroOrdens.Aplicacao.UserCase.CriarOrdem;
using LivroOrdens.Aplicacao.UserCase.ObterLivrosOrdens;
using LivroOrdens.Dominio.Enum;
using LivroOrdens.Fix.Cliente.Session;
using LivroOrdens.Fix.Constantes;
using QuickFix.Fields;
using QuickFix.FIX44;
using System.Collections.Concurrent;

namespace LivroOrdens.Fix.Cliente.Services
{
    public class FixClienteService : IFixClienteService
    {
        private readonly ClienteFixSession _clienteFixSession;
        private static readonly ConcurrentDictionary<string, TaskCompletionSource<ObterLivrosOrdensResponse>> _snapshotsPendentes = new();

        public FixClienteService(ClienteFixSession clienteFixSession)
        {
            _clienteFixSession = clienteFixSession;
        }

        public Task<CriarOrdemResponse> EnviarNovaOrdem(CriarOrdermRequest request)
        {
            if (_clienteFixSession.SessionID is null)
            {
                return Task.FromResult(new CriarOrdemResponse
                {
                    Sucesso = false,
                    Mensagem = "Sessão FIX não está conectada."
                });
            }

            var lado = ObterSide(request.Lado);

            var mensagem = new NewOrderSingle(new ClOrdID(request.CIOrdId), new Symbol(request.Simbolo), new Side(lado), new TransactTime(DateTime.UtcNow), new OrdType(OrdType.LIMIT));
            mensagem.Set(new OrderQty(request.Quantidade));
            mensagem.Set(new Price(request.Preco));

            QuickFix.Session.SendToTarget(mensagem, _clienteFixSession.SessionID);

            return Task.FromResult(new CriarOrdemResponse
            {
                Sucesso = true,
                Mensagem = "Ordem enviada para o processamento."
            });
        }

        public Task<CancelarOrdemResponse> CancelarOrdem(CancelarOrdemRequest request)
        {

            if (_clienteFixSession.SessionID is null)
            {
                return Task.FromResult(new CancelarOrdemResponse
                {
                    Sucesso = false,
                    Mensagem = "Sessão FIX não está conectada."
                });
            }

            var lado = ObterSide(request.Lado);

            var messagem = new OrderCancelRequest(new OrigClOrdID(request.OrigClOrdId), new ClOrdID(request.ClOrdId), new Symbol(request.Simbolo), new Side(lado), new TransactTime(DateTime.UtcNow));

            QuickFix.Session.SendToTarget(messagem, _clienteFixSession.SessionID);

            return Task.FromResult(new CancelarOrdemResponse
            {
                Sucesso = true,
                Mensagem = "Solicitação de cancelamento enviada com sucesso."
            });
        }

        public Task<ObterLivrosOrdensResponse> ObterLivrosOrdens(ObterLivrosOrdensRequest request)
        {
            return Task.FromResult(new ObterLivrosOrdensResponse
            {
                Sucesso = true,
                Mensagem = "Livros de ordens obtidos com sucesso.",
                LivrosOrdens = []
            });
        }

        private static char ObterSide(EAcaoOrdem acaoOrdem) => EAcaoOrdem.Comprar == acaoOrdem ? Side.BUY : Side.SELL;

        public async Task<ObterLivrosOrdensResponse> ObterLivroOrdens()
        {
            var sessionID = _clienteFixSession.SessionID;

            if (sessionID is null)
            {
                return new ObterLivrosOrdensResponse
                {
                    Sucesso = false,
                    Mensagem = "Sessão FIX não está conectada."
                };
            }

            var requestId = Guid.NewGuid().ToString();

            var task = new TaskCompletionSource<ObterLivrosOrdensResponse>(TaskCreationOptions.RunContinuationsAsynchronously);

            _snapshotsPendentes.TryAdd(requestId, task);

            var mensagem = new QuickFix.Message();

            mensagem.Header.SetField(new MsgType(FixCustomeMessageTypes.BookSnapshotRequest));
            mensagem.SetField(new StringField(9001, requestId));

            QuickFix.Session.SendToTarget(mensagem, sessionID);

            return await task.Task.WaitAsync(TimeSpan.FromSeconds(5));
        }

        public static void CompletarSnapshotLivro(string requestId, ObterLivrosOrdensResponse response)
        {
            if (_snapshotsPendentes.TryRemove(requestId, out var tcs))
            {
                tcs.SetResult(response);
            }
        }
    }
}
