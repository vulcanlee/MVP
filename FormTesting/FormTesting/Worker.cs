using Microsoft.Extensions.Options;
using TestingModel.Models;

namespace FormTesting
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger,
            IOptions<TestingTargetConfiguration> TargetOption,
            IOptions<List<TestingNodeConfiguration>> TestingNodeOption)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}