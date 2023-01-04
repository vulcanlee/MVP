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
        List<Task<HttpClient>> clientsTask = new List<Task<HttpClient>>();
        List<Task<string>> tasks = new List<Task<string>>();
        Stopwatch stopwatch = new Stopwatch();

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
            int index = 0;

            this.testingNode = testingNodeConfiguration;

            #region 設定這個方法會用到的相關欄位值
            FormInformation formInformation = new FormInformation();
            formInformation.ConvertConfigurationToFormInformation(testingNode);
            #endregion

            #region 建立需要測試的表單清單 URL
            formHelper.MakeFormUrl(testingNode, formInformation);
            #endregion

            await remotePerformanceHelper.CleanRemotePerformanceMeasureDataAsync(testingNode);

            PerformanceMeasureHeader performanceMeasureHeader =
                performanceMeasure.NewHeader();

            clients = await formHelper.MakeHasLoginHttpClient(testingNode,
                formInformation, performanceMeasureHeader);

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

            #region 列印出效能量測結果
            if (testingNode.HttpClientPerformanceMeasure == true)
                performanceMeasure.Output(SortEnum.TotalCost, formInformation.AllFormsTitle.ToArray());
            #endregion

            #region 列印效能統計分析
            if (testingNode.RemotePerformanceMeasure == true)
            {
                var performanceMeasureResult = await GetPerformanceMeasureAsync();
                performanceMeasure.ParsePerformance(performanceMeasureResult);
                if (testingNode.RemotePerformanceMaxLatencyAnalysis)
                    performanceMeasure.OutputMaxLatencyAnalysis(performanceMeasureResult);
                if (testingNode.RemotePerformanceOutputDetail)
                    performanceMeasure.OutputDetail(performanceMeasureResult);
                if (testingNode.RemotePerformanceOutputNodeDetail)
                    performanceMeasure.OutputNodeDetail(performanceMeasureResult);
            }
            #endregion

            return;
        }


        async Task<List<PerformanceMeasureHeader>> GetPerformanceMeasureAsync()
        {
            var endPoint = $"{testingNode.Host.ConnectHost}" +
                $"{testingNode.GetRemotePerformanceMeasureEndpoint}";
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            HttpClient client = new HttpClient(handler);
            var content = await client.GetStringAsync(endPoint);
            var result = JsonConvert.DeserializeObject<List<PerformanceMeasureHeader>>(content);
            return result;
        }

        async Task<string> NetGetFormAsync(PerformanceMeasureHeader measure,
        HttpClient client, string formEndPoint, int index, bool distributionTesting,
       bool performanceMeasureAction, TestingNodeConfiguration testingNode)
        {
            Random random = new Random();
            int timeRange = 30 * 1000;
            PerformanceMeasureNode measureItem = null;
            string title = "";

            try
            {
                if (distributionTesting)
                {
                    int waitMS = random.Next(timeRange);
                    await Task.Delay(waitMS);
                }

                if (performanceMeasureAction == true)
                    measureItem = measure
                        .BeginMeasure($"Get Form Content Page {index}", performanceMeasureAction);
                var beforeResponse = await client.GetAsync(formEndPoint);
                var html = await beforeResponse.Content.ReadAsStringAsync();

                #region 取得 Title
                var beginIndex = html.IndexOf("<title>");
                var endIndex = html.IndexOf("</title>");
                title = html.Substring(beginIndex, endIndex - beginIndex).Replace("<title>", "");
                //Console.Write(title+"  ");
                if (performanceMeasureAction == true)
                    measureItem.Title = $"{measureItem.Title} > {title} ";
                #endregion

                if (performanceMeasureAction == true)
                    measure.EndMeasure(measureItem!, performanceMeasureAction);

                #region 將此次讀取到的網頁儲存到本機 Data 目錄下
                if (testingNode.LogFormRawHtml)
                {
                    var foo = Directory.GetCurrentDirectory();
                    var dataFolder = Path.Combine(Directory.GetCurrentDirectory(),
                        MagicObject.OutputFormHtmlFolderName);
                    if (Directory.Exists(dataFolder) == false) Directory.CreateDirectory(dataFolder);
                    var filename = Path.Combine(dataFolder, $"forms_{index}.html");
                    await File.WriteAllTextAsync(filename, html);
                }
                #endregion
            }
            catch (Exception ex)
            {
                logger!.LogError(ex, $"Access URL : {formEndPoint}");
                Console.WriteLine(ex.Message);
            }
            return title;
        }
    }
}