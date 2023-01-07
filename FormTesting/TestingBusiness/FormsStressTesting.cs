using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using TestingBusiness.Helpers;
using TestingBusiness.Services;
using TestingModel.Enums;
using TestingModel.Magics;
using TestingModel.Models;

namespace TestingBusiness;

public class FormsStressTesting
{
    private readonly PerformanceMeasure performanceMeasure;
    private readonly ILogger<FormsStressTesting> logger;
    private readonly FormService formHelper;
    private readonly RemotePerformanceService remotePerformanceHelper;
    private TestingNodeConfiguration testingNode = new();
    List<HttpClient> clients = new List<HttpClient>();

    public FormsStressTesting(PerformanceMeasure performanceMeasure,
        ILogger<FormsStressTesting> logger,
        FormService formHelper, RemotePerformanceService remotePerformanceHelper)
    {
        this.performanceMeasure = performanceMeasure;
        this.logger = logger;
        this.formHelper = formHelper;
        this.remotePerformanceHelper = remotePerformanceHelper;
    }

    /// <summary>
    /// 開始進行設定檔案中規劃的動作
    /// </summary>
    /// <param name="testingNodeConfiguration"></param>
    /// <returns></returns>
    public async Task NETFormRunningAsync(CancellationToken cancellationToken,
        TestingNodeConfiguration testingNodeConfiguration)
    {
        // 讓等下要顯示文字可以正常顯示出來
        await Task.Delay(1500);
        Console.WriteLine();
        Console.WriteLine();

        this.testingNode = testingNodeConfiguration;

        FormInformation formInformation = await new FormInformation()
            .ConvertConfigurationToFormInformation(testingNode);

        formHelper.MakeFormUrl(testingNode, formInformation);

        if (cancellationToken.IsCancellationRequested) return;
        await remotePerformanceHelper.CleanRemotePerformanceMeasureDataAsync(testingNode);

        PerformanceMeasureHeader performanceMeasureHeader =
            performanceMeasure.NewHeader();

        if (cancellationToken.IsCancellationRequested) return;
        clients = await formHelper.MakeHasLoginHttpClient(cancellationToken, testingNode,
            formInformation, performanceMeasureHeader);

        #region 決定此程式的運作模式
        if (formInformation.Mode == TestingModeEnum.表單暖機預先載入)
        {
            if (cancellationToken.IsCancellationRequested) return;
            await formHelper.WarmingUpForms(cancellationToken, testingNode,
                performanceMeasureHeader, formInformation,
                clients);
        }
        else if (formInformation.Mode == TestingModeEnum.壓力測試 ||
            formInformation.Mode == TestingModeEnum.時間內吞吐量測試)
        {
            if (cancellationToken.IsCancellationRequested) return;
            await formHelper.StressPerformanceForms(cancellationToken, testingNode,
                performanceMeasureHeader, formInformation, clients);
        }
        #endregion

        if (cancellationToken.IsCancellationRequested) return;
        remotePerformanceHelper.PrintHttpClientPerformanceResult(testingNode,
            performanceMeasure, formInformation);

        if (cancellationToken.IsCancellationRequested) return;
        await remotePerformanceHelper.PrintRemotePerformanceMeasureResultAsync(testingNode, performanceMeasure);

        logger.LogInformation("正常執行完畢 ----------------------------------");
        return;
    }
}