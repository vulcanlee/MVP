using Microsoft.Extensions.Options;
using TestingBusiness;
using TestingBusiness.Helpers;
using TestingModel.Models;

namespace FormTesting
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptions<TestingTargetConfiguration> targetOption;
        private readonly IOptions<List<TestingNodeConfiguration>> testingNodeOption;
        private readonly FormsStressTesting formsStressTesting;
        private readonly FormHelper formHelper;

        public Worker(ILogger<Worker> logger,
            IOptions<TestingTargetConfiguration> TargetOption,
            IOptions<List<TestingNodeConfiguration>> TestingNodeOption,
            FormsStressTesting formsStressTesting,
            FormHelper formHelper)
        {
            _logger = logger;
            targetOption = TargetOption;
            testingNodeOption = TestingNodeOption;
            this.formsStressTesting = formsStressTesting;
            this.formHelper = formHelper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            TestingNodeConfiguration node = formHelper.GetFormConfigurationNode();

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