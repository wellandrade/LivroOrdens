using LivroOrdens.Fix.Cliente.Application;
using Microsoft.Extensions.Hosting;
using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;
using QuickFix.Transport;

namespace LivroOrdens.Fix.Cliente.HostedService
{
    public class ClienteFixHostedService : IHostedService
    {
        private readonly ClienteFixApplication _application;
        private IInitiator? _initiator;

        public ClienteFixHostedService(ClienteFixApplication application)
        {
            _application = application;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "Cliente", "Config", "cliente.cfg");

            var settings = new SessionSettings(caminhoArquivo);
            var storeFactory = new FileStoreFactory(settings);
            var logFactory = new FileLogFactory(settings);
            var messageFactory = new DefaultMessageFactory();

            _initiator = new SocketInitiator(_application, storeFactory, settings, logFactory, messageFactory);
            _initiator.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _initiator?.Stop();
            return Task.CompletedTask;
        }
    }
}
