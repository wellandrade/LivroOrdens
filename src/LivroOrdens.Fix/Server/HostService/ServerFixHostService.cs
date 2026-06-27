using LivroOrdens.Fix.Server.Application;
using Microsoft.Extensions.Hosting;
using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;

namespace LivroOrdens.Fix.Server.HostService
{
    public class ServerFixHostService : IHostedService
    {
        private readonly ServerFixApplication _serverFixApplication;
        private IAcceptor? _acceptor;

        public ServerFixHostService(ServerFixApplication serverFixApplication)
        {
            _serverFixApplication = serverFixApplication;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "Server", "Config", "servidor.cfg");

            var settings = new SessionSettings(caminhoArquivo);
            var storeFactory = new FileStoreFactory(settings);
            var logFactory = new FileLogFactory(settings);
            var messageFactory = new DefaultMessageFactory();

            _acceptor = new ThreadedSocketAcceptor(_serverFixApplication, storeFactory, settings, logFactory, messageFactory);

            _acceptor.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _acceptor?.Stop();
            return Task.CompletedTask;
        }
    }
}
