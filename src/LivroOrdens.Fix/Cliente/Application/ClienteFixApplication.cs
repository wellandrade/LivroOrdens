using LivroOrdens.Aplicacao.UserCase.ObterLivrosOrdens;
using LivroOrdens.Fix.Cliente.Services;
using LivroOrdens.Fix.Cliente.Session;
using LivroOrdens.Fix.Constantes;
using QuickFix;
using QuickFix.Fields;
using System.Text.Json;

namespace LivroOrdens.Fix.Cliente.Application
{
    public class ClienteFixApplication : IApplication
    {
        private readonly ClienteFixSession _clienteFixSession;

        public ClienteFixApplication(ClienteFixSession clienteFixSession)
        {
            _clienteFixSession = clienteFixSession;
        }

        public void FromAdmin(Message message, SessionID sessionID)
        {
        }

        public void FromApp(Message message, SessionID sessionID)
        {
            var msgType = message.Header.GetString(Tags.MsgType);

            switch (msgType)
            {
                case FixCustomeMessageTypes.BookSnapshotResponse:
                    ProcessarRespostaSnapshotLivro(message);
                    break;
            }
        }

        public void OnCreate(SessionID sessionID)
        {
        }

        public void OnLogon(SessionID sessionID)
        {
            _clienteFixSession.SessionID = sessionID;
        }

        public void OnLogout(SessionID sessionID)
        {
            _clienteFixSession.SessionID = null;
        }

        public void ToAdmin(Message message, SessionID sessionID)
        {
        }

        public void ToApp(Message message, SessionID sessionID)
        {
        }

        private void ProcessarRespostaSnapshotLivro(Message message)
        {
            try
            {
                var requestId = message.GetString(9001);
                var json = message.GetString(9002);

                var response = JsonSerializer.Deserialize<ObterLivrosOrdensResponse>(json)
                ?? new ObterLivrosOrdensResponse
                {
                    Sucesso = false,
                    Mensagem = "Não foi possível desserializar o snapshot do livro."
                };

                FixClienteService.CompletarSnapshotLivro(requestId, response);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
