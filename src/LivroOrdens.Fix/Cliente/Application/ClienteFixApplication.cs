using LivroOrdens.Fix.Cliente.Session;
using QuickFix;
using QuickFix.Fields;

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
    }
}
