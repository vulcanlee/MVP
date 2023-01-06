using Microsoft.Extensions.Options;
using TestingBusiness;
using TestingBusiness.Services;
using TestingModel.Models;

namespace FormTesting;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IOptions<TestingTargetConfiguration> targetOption;
    private readonly FormsStressTesting formsStressTesting;
    private readonly FormService formHelper;

    public Worker(ILogger<Worker> logger,
        IOptions<TestingTargetConfiguration> TargetOption,
        FormsStressTesting formsStressTesting,
        FormService formHelper)
    {
        _logger = logger;
        targetOption = TargetOption;
        this.formsStressTesting = formsStressTesting;
        this.formHelper = formHelper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        TestingNodeConfiguration? node = formHelper.GetCurrentFormConfigurationNode();

        if (node == null)
        {
            Console.WriteLine($"在設定檔案內，無法發現到要測試的目標 {targetOption.Value.TestingNode}");
            Environment.Exit(1);
            return;
        }

        try
        {
            await formsStressTesting.NETFormRunningAsync(stoppingToken, node);

        }
        catch (Exception)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"使用者發出取消執行請求");
            }
            else
            {
                throw;
            }
        }

        Environment.Exit(0);
    }
}