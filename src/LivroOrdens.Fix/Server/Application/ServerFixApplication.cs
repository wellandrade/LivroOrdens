using LivroOrdens.Aplicacao.UserCase.CancelarOrdem;
using LivroOrdens.Aplicacao.UserCase.CriarOrdem;
using LivroOrdens.Aplicacao.UserCase.ObterLivrosOrdens;
using LivroOrdens.Dominio.Enum;
using LivroOrdens.Fix.Cliente.Session;
using LivroOrdens.Fix.Constantes;
using QuickFix;
using QuickFix.Fields;

namespace LivroOrdens.Fix.Server.Application
{
    public class ServerFixApplication : IApplication
    {
        private readonly CriarOrdemUseCase _criarOrdemUseCase;
        private readonly CancelarOrdemUseCase _cancelarOrdemUseCase;
        private readonly ObterLivrosOrdensUseCase _obterLivrosOrdensUseCase;

        public ServerFixApplication(CriarOrdemUseCase criarOrdemUseCase,
            CancelarOrdemUseCase cancelarOrdemUseCase,
            ObterLivrosOrdensUseCase obterLivrosOrdensUseCase)
        {
            _criarOrdemUseCase = criarOrdemUseCase;
            _cancelarOrdemUseCase = cancelarOrdemUseCase;
            _obterLivrosOrdensUseCase = obterLivrosOrdensUseCase;
        }

        public void FromAdmin(Message message, SessionID sessionID) {
        }

        public async void FromApp(Message message, SessionID sessionID)
        {
            var tipoMensagem = message.Header.GetString(Tags.MsgType);

            switch (tipoMensagem)
            {
                case MsgType.NEWORDERSINGLE:
                    await ProcessarNovaOrdem(message, sessionID); 
                    break;
                case MsgType.ORDERCANCELREQUEST:
                    await ProcessarCancelamentoOrdem(message, sessionID);
                    break;
                case FixCustomeMessageTypes.BookSnapshotRequest:
                    await ProcessarSnapShotLivro(message, sessionID);
                    break;
                default:
                    break;
            }
        }

        public void OnCreate(SessionID sessionID) { }

        public void OnLogon(SessionID sessionID) {
        }

        public void OnLogout(SessionID sessionID) { }

        public void ToAdmin(Message message, SessionID sessionID) { }

        public void ToApp(Message message, SessionID sessionID) { }

        private async Task ProcessarNovaOrdem(Message mensagem, SessionID sessionID)
        {
            var request = new CriarOrdermRequest
            {
                CIOrdId = mensagem.GetString(Tags.ClOrdID),
                Simbolo = mensagem.GetString(Tags.Symbol),
                Quantidade = mensagem.GetInt(Tags.OrderQty),
                Preco = mensagem.GetDecimal(Tags.Price),
                Lado = mensagem.GetChar(Tags.Side) == Side.BUY ? EAcaoOrdem.Comprar : EAcaoOrdem.Vender
            };

            var response = await _criarOrdemUseCase.Executar(request);

            var (execType, ordStatus) = ObterStatus(response.Sucesso, false);

            var leavesQuantity = response.Sucesso ? request.Quantidade : 0;

            EnviarExecutionReport(sessionID, request.CIOrdId, request.Simbolo, request.Lado, request.Quantidade, response.Mensagem, execType, ordStatus, leavesQuantity);
        }

        private async Task ProcessarCancelamentoOrdem(Message mensagem, SessionID sessionID)
        {
            var request = new CancelarOrdemRequest
            {
                OrigClOrdId = mensagem.GetString(Tags.OrigClOrdID),
                ClOrdId = mensagem.GetString(Tags.ClOrdID),
                Simbolo = mensagem.GetString(Tags.Symbol),
                Lado = mensagem.GetChar(Tags.Side) == Side.BUY ? EAcaoOrdem.Comprar : EAcaoOrdem.Vender
            };

            var response = await _cancelarOrdemUseCase.Executar(request);
            var (execType, ordStatus) = ObterStatus(response.Sucesso, true);

            EnviarExecutionReport(sessionID, request.ClOrdId, request.Simbolo, request.Lado, 0, response.Mensagem, execType, ordStatus, 0);
        }

        private void EnviarExecutionReport(SessionID sessionID, string clOrdId, string simbolo, EAcaoOrdem lado, decimal quantidade, string mensagem, char execType, char ordStatus, decimal leavesQuantity)
        {
            var executionReport = new QuickFix.FIX44.ExecutionReport(
                new OrderID(Guid.NewGuid().ToString()),
                new ExecID(Guid.NewGuid().ToString()),
                new ExecType(execType),
                new OrdStatus(ordStatus),
                new Symbol(simbolo),
                new Side(lado == EAcaoOrdem.Comprar ? Side.BUY : Side.SELL),
                new LeavesQty(leavesQuantity),
                new CumQty(0), 
                new AvgPx(0));

            executionReport.Set(new ClOrdID(clOrdId));
            executionReport.Set(new Text(mensagem));

            Session.SendToTarget(executionReport, sessionID);
        }

        public async Task ProcessarSnapShotLivro(Message message, SessionID sessionID) 
        {
            var requestId = message.GetString(9001);

            var resultado = await _obterLivrosOrdensUseCase.Executar();

            var json = System.Text.Json.JsonSerializer.Serialize(resultado);

            var resposta = new Message();
            resposta.Header.SetField(new MsgType(FixCustomeMessageTypes.BookSnapshotResponse));
            resposta.SetField(new StringField(9001, requestId));
            resposta.SetField(new StringField(9002, json));

            Session.SendToTarget(resposta, sessionID);
        }

        private static (char execType, char ordStatus) ObterStatus(bool sucesso, bool cancelamento)
        {
            if (cancelamento)
            {
                return sucesso ? (ExecType.CANCELED, OrdStatus.REJECTED) : (ExecType.REJECTED, OrdStatus.REJECTED);
            }

            return sucesso ? (ExecType.NEW, OrdStatus.NEW) : (ExecType.REJECTED, OrdStatus.REJECTED);
        }
    }
}
;