namespace LivroOrdens.Client.Worker
{
    public class Worker(ILogger<Worker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("**** Cliente FIX Worker iniciado. ****");

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
