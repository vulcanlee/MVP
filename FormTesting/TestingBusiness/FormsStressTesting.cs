using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using TestingBusiness.Helpers;
using TestingBusiness.Services;
using TestingModel.Enums;
using TestingModel.Magics;
using TestingModel.Models;

namespace TestingBusiness
{
    public class FormsStressTesting
    {
        private readonly PerformanceMeasure performanceMeasure;
        private readonly ILogger<FormsStressTesting> logger;
        private readonly FormService formHelper;
        private readonly RemotePerformanceService remotePerformanceHelper;
        private TestingNodeConfiguration testingNode;
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
        public async Task NETFormsAsync(TestingNodeConfiguration testingNodeConfiguration)
        {
            await Task.Delay(1500);
            Console.WriteLine();
            Console.WriteLine();
            int index = 0;

            this.testingNode = testingNodeConfiguration;

            #region 設定這個方法會用到的相關欄位值
            FormInformation formInformation = new FormInformation();
            formInformation.ConvertConfigurationToFormInformation(testingNode);
            #endregion

            formHelper.MakeFormUrl(testingNode, formInformation);

            await remotePerformanceHelper.CleanRemotePerformanceMeasureDataAsync(testingNode);

            PerformanceMeasureHeader performanceMeasureHeader =
                performanceMeasure.NewHeader();

            clients = await formHelper.MakeHasLoginHttpClient(testingNode,
                formInformation, performanceMeasureHeader);

            #region 決定此程式的運作模式
            if (formInformation.Mode == TestingModeEnum.表單暖機預先載入)
            {
                await formHelper.WarmingUpForms(testingNode,
                    performanceMeasureHeader, formInformation,
                    clients);
            }
            else if (formInformation.Mode == TestingModeEnum.壓力測試 ||
                formInformation.Mode == TestingModeEnum.時間內吞吐量測試)
            {
                await formHelper.StressPerformanceForms(testingNode,
                    performanceMeasureHeader, formInformation, clients);
            }
            #endregion

            await remotePerformanceHelper.PrintHttpClientPerformanceResult(testingNode, 
                performanceMeasure, formInformation);

            await remotePerformanceHelper.PrintRemotePerformanceMeasureResult(testingNode,performanceMeasure);

            return;
        }
    }
}