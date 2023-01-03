using Microsoft.Extensions.Options;
using TestingBusiness;
using TestingModel.Models;

namespace FormTesting
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptions<TestingTargetConfiguration> targetOption;
        private readonly IOptions<List<TestingNodeConfiguration>> testingNodeOption;
        private readonly FormsStressTesting formsStressTesting;

        public Worker(ILogger<Worker> logger,
            IOptions<TestingTargetConfiguration> TargetOption,
            IOptions<List<TestingNodeConfiguration>> TestingNodeOption,
            FormsStressTesting formsStressTesting)
        {
            _logger = logger;
            targetOption = TargetOption;
            testingNodeOption = TestingNodeOption;
            this.formsStressTesting = formsStressTesting;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TestingNodeConfiguration node = null;
            foreach (var item in testingNodeOption.Value)
            {
                if (targetOption.Value.TestingNode.ToLower() ==
                    item.Title.ToLower())
                {
                    node = item;
                    break;
                }
            }

            if (node == null)
            {
                Console.WriteLine($"在設定檔案內，無法發現到要測試的目標 {targetOption.Value.TestingNode}");
                Environment.Exit(1);
                return;
            }
            await formsStressTesting.NETFormsAsync(node);
            Environment.Exit(0);
        }
    }
}